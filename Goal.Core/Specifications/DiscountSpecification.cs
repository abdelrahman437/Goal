using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Core.DTO;
using Goal.Core.Helpers;
using Goal.Core.Models;

namespace Goal.Core.Specifications
{
    public class DiscountSpecification : BaseSpecification<Discount>
    {
        public DiscountSpecification(
            DiscountQueryParameters Params,
            bool AsTracking = true
            )
        {


            predicate = p =>
                (!Params.minValue.HasValue || p.Value >= Params.minValue) &&
                (!Params.maxValue.HasValue || p.Value <= Params.maxValue) &&
                (!Params.Start.HasValue || p.StartDate >= Params.Start) &&
                (!Params.End.HasValue || p.EndDate <= Params.End) &&
                (string.IsNullOrEmpty(Params.Name) || p.Name.ToLower().Contains(Params.Name.ToLower()));


            switch (Params.orderBy)
            {
                case Ordering.ASC:
                    ApplyOrderBy(q => q.OrderBy(p => p.Value));
                    break;
                case Ordering.DESC:
                    ApplyOrderBy(q => q.OrderByDescending(p => p.Value));
                    break;
            }
            if (Params.skip.HasValue)
                ApplyPaging(((Params.skip - 1) * Params.Take), Params.Take);
            else
                ApplyPaging(0, Params.Take);
            if (!AsTracking)
                AsNoTracking();

        }
    }
}
