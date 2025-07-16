using System.Linq.Expressions;
using Goal.Core.DTO;
using Goal.Core.Helpers;
using Goal.Core.Models;

namespace Goal.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(
            ProductQueryParameters Params,
            bool AsTracking = true,
            bool QueryFilter = true)
        {
            if (Params.minPrice.HasValue && Params.maxPrice.HasValue && Params.minPrice > Params.maxPrice)
            {
                var temp = Params.minPrice;
                Params.minPrice = Params.maxPrice;
                Params.maxPrice = temp;
            }

            predicate = p =>
                (!Params.categoryId.HasValue || p.CategoryId == Params.categoryId) &&
                (!Params.brandId.HasValue || p.BrandId == Params.brandId) &&
                (!Params.minPrice.HasValue || p.Price >= Params.minPrice) &&
                (!Params.maxPrice.HasValue || p.Price <= Params.maxPrice) &&
                (!Params.discountOnly || p.DiscountId != null) &&
                (!Params.discountOnly || p.Discount.IsActive == true) &&
                (string.IsNullOrEmpty(Params.name) || p.Name.ToLower().Contains(Params.name.ToLower()));

            AddInclude(p => p.Images);
            AddInclude(p => p.Discount);
            AddInclude(p => p.Stock);

            if (!AsTracking)
                AsNoTracking();

            if(!QueryFilter)
                IgnoreQueryFilter();
            if (Params.skip.HasValue && Params.Take.HasValue)
                ApplyPaging(((Params.skip - 1) * Params.Take), Params.Take);

            switch (Params.orderBy)
            {
                case Ordering.ASC:
                    ApplyOrderBy(q=>q.OrderBy(p=>p.DiscountedPrice));
                    break;
                case Ordering.DESC:
                    ApplyOrderBy(q => q.OrderByDescending(p => p.DiscountedPrice));
                    break;
            }
        }
        public ProductSpecification(int id,
            bool AsTracking = true,
            bool QueryFilter = true,
            params Expression<Func<Product, object>>[] Includes)
        {
            predicate = p => p.Id == id;
            if (Includes != null)
            {
                foreach (var include in Includes)
                    AddInclude(include);
            }
            if (!AsTracking)
                AsNoTracking();

            if (!QueryFilter)
                IgnoreQueryFilter();
        }
    }
}
