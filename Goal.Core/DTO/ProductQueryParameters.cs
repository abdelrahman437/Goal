using Goal.Core.Helpers;

namespace Goal.Core.DTO
{
    public class ProductQueryParameters
    {
        public int? categoryId { get; set; } = null;
        public int? brandId { get; set; } = null;
        public int? discountId { get; set; } = null;
        public decimal? minPrice { get; set; } = null;
        public decimal? maxPrice { get; set; } = null;
        public Ordering orderBy { get; set; } = Ordering.Null;
        public int? skip { get; set; } = null;
        public int? Take { get; set; } = null;
        public string? name { get; set; }  = null;
        public bool discountOnly { get; set; } = false;
    }
}
