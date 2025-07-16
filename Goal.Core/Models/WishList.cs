using System.ComponentModel.DataAnnotations;

namespace Goal.Core.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        public DateTime CreateAt { get; set; }

        public virtual List<Product> Products { get; set; } = new();
    }
}
