using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Goal.Core.Models
{
    public class Customer : IdentityUser<int>
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 25 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 25 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birth date is required.")]
        public DateOnly BirthDate { get; set; }

        [Required(ErrorMessage = "Image path is required.")]
        public string ImgePath { get; set; }
        public string? PhotoPublicId { get; set; }

        public int AddressId { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual List<CartItem> CartItems { get; set; } = new();
        public virtual Address CustomerAddress { get; set; } = new();
        public virtual List<Order> Orders { get; set; } = new();
    }
}
