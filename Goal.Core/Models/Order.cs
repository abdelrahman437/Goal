using System.ComponentModel.DataAnnotations;

namespace Goal.Core.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Payment type (IsCash) is required.")]
        public bool IsCash { get; set; }

        [Required(ErrorMessage = "Created date is required.")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Address ID is required.")]
        public int AddressId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new();
    }
}
