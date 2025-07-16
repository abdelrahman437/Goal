using System.Collections;
using System.ComponentModel.DataAnnotations;
using Goal.Core.Models;
namespace Goal.Core.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MaxLength(400)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Stock stock { get; set; }
        public Discount? Discount { get; set; }
        public List<Image> images { get; set; }
    }
}
