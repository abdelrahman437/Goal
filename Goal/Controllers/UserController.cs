using Goal.Core.DTO;
using Goal.Core.Interfaces.Services;
using Goal.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Goal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _config;
        public UserController(IAuthServices authServices, UserManager<Customer> userManager, IConfiguration config)
        {
            _authServices = authServices;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm]RegisterDTO dTO)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

                var result = await _authServices.RegisterAsync(dTO);

                return Ok(result.Massage);
          
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm]LoginDTO dTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.login(dTO);

            if (!result.IsAuthenticated)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }



        [HttpGet("conferm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string Email,string token)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null) 
                return NotFound("User Not found");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
                return BadRequest("fail");

            return Redirect(_config["RedirictSetting:AfterEmailConfirmation"]);
        }
    }
}
