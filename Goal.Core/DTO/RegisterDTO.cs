using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Goal.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 25 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 25 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Birth date is required.")]
        public DateOnly BirthDate { get; set; }
        [EmailAddress(ErrorMessage ="Invalid Email format")]
        [Required(ErrorMessage = "Email Address Is requierd")]
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Is requierd")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Is requierd")]
        [Compare("Password",ErrorMessage ="Password Don't match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 100 characters.")]
        public string address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "City must be between 5 and 255 characters.")]
        public string City { get; set; }
    }
}
