using System.ComponentModel.DataAnnotations;

namespace Goal.Core.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quntity { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
