using System.ComponentModel.DataAnnotations;

namespace Goal.Core.DTO
{
    public class DiscountDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Value is required.")]
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public decimal Value { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }
    }
}
