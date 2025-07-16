using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Goal.Core.DTO
{
    public class AddProductDTO
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Name must be at least 5 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(400, ErrorMessage = "Description can't exceed 400 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Brand ID is required.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }

        public int? DiscountId { get; set; }

        [Required(ErrorMessage = "Stock data is required.")]
        public StockDTO stock { get; set; }

        [Required(ErrorMessage = "At least one image is required.")]
        public List<IFormFile> formFiles { get; set; } = new();
    }
}
