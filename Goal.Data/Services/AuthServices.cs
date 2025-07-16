using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Goal.Core.DTO;
using Goal.Core.Helpers;
using Goal.Core.Interfaces.Services;
using Goal.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Goal.Data.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IMapper _mapper;
        private readonly JWT _jwt;
        private readonly EmailService _emailService;
        private readonly IImageServices _img;

        public AuthServices(UserManager<Customer> userManager, IMapper mapper, IOptions<JWT> jwt, EmailService emailService,IImageServices imageServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwt = jwt.Value;
            _emailService = emailService;
            _img = imageServices;
        }
        public async Task<AuthModel> RegisterAsync(RegisterDTO registerModel)
        {
            if (await _userManager.FindByEmailAsync(registerModel.Email) is not null)
                return new AuthModel { Massage = "Email is already registerd" };


            var User = new Customer();
            _mapper.Map(registerModel, User);

            var photo = await _img.UploadImageAsync(registerModel.Photo);
            User.ImgePath = photo.SecureUrl.ToString();
            User.PhotoPublicId = photo.PublicId;

            User.UserName = registerModel.Email.Split("@")[0];
            var result = await _userManager.CreateAsync(User, registerModel.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description}, ";

                return new AuthModel { Massage = errors };
            }

            await _userManager.AddToRoleAsync(User, "User");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);
            var encodeToken = WebUtility.UrlEncode(token);

            var confirmLink = $"http://localhost:44975/api/User/conferm-email?Email={User.Email}&token={encodeToken}";



            await _emailService.SendActivationEmail(User.Email, User.UserName, confirmLink);

            return new AuthModel { Massage = "Check Your Mail" };
        }
        public async Task<AuthModel> login(LoginDTO login)
        {
            var user =
                     await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
                return new AuthModel() { Massage = "The Email Not Register" };

            bool found =
                        await _userManager.CheckPasswordAsync(user, login.Password);
            if (!found)
                return new AuthModel() { Massage = "Email or Password Invalid" };

            if (!user.EmailConfirmed)
                return new AuthModel() { Massage = "Active your Account first" };

            var roles = await _userManager.GetRolesAsync(user); 
            var token = await CreateToken(user);
            return new AuthModel
            {
                Email = user.Email,
                Username = user.UserName,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireOn = token.ValidTo,
                Roles = roles
            };

        }

        private async Task<JwtSecurityToken> CreateToken(Customer user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uId",user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);


            var key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

            SigningCredentials signingCredentials =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                audience: _jwt.Audience,
                issuer: _jwt.Issuer,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return token;
        }



    }
}
