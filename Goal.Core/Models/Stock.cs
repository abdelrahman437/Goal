using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Goal.Core.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Sold is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Sold must be a non-negative number.")]
        public int Sold { get; set; } = 0;

        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
