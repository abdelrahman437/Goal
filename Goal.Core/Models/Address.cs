using System.ComponentModel.DataAnnotations;

namespace Goal.Core.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 100 characters.")]
        public string address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "City must be between 5 and 255 characters.")]
        public string City { get; set; }
        public virtual Order? Orders { get; set; }
        public virtual Customer? customer { get; set; } 


    }
}
