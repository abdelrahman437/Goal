using System.Linq.Expressions;
using Goal.Core.DTO;
using Goal.Core.Models;

namespace Goal.Core.Helpers
{
    public class DiscountFilter
    {
        public DiscountFilter(string v1, bool v2)
        {
            this.massage = v1;
            this.Erorr = v2;
        }

        public string? massage { get; set; }
        public bool Erorr { get; set; }
    }
    public static class Helpers
    {
        public static DiscountFilter DiscountHelper(this DiscountDTO discount)
        {
            if (discount.EndDate < discount.StartDate)
                return new DiscountFilter("End date shoud be greater than start date", true);
            if (discount.IsActive)
            {
                if ((discount.StartDate >= DateTime.Now || discount.EndDate < DateTime.Now))
                    return (new DiscountFilter("discount shoud be not active", true));
            }
            if (discount.Value <= 0)
                return new DiscountFilter(null,true);
            return new DiscountFilter(null, false);
        }
        
    }

}
