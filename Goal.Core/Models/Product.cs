using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Goal.Core.Common;

namespace Goal.Core.Models
{
    public class Product : ISoftDeleteable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name must be at least 5 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(400, ErrorMessage = "Description can't exceed 400 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Discounted price must be a positive value.")]
        public decimal DiscountedPrice { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        public int StockId { get; set; }

        public int? DiscountId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        // Navigation Properties
        public virtual Discount? Discount { get; set; }
        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Stock Stock { get; set; }

        public virtual List<CartItem> Items { get; set; } = new();
        public virtual List<WishList> WishLists { get; set; } = new();
        public virtual List<Image> Images { get; set; } = new();
    }
}
