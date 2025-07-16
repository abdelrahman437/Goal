using System.ComponentModel.DataAnnotations;

namespace Goal.Core.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(55, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 55 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(255, MinimumLength = 15, ErrorMessage = "Email must be between 15 and 255 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message content is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Sent date is required.")]
        public DateTime SentAt { get; set; }
    }
}
