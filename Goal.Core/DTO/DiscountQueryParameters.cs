using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Core.Helpers;

namespace Goal.Core.DTO
{
    public class DiscountQueryParameters
    {
        public string? Name { get; set; } = null;
        public decimal? minValue { get; set; } = null;
        public decimal? maxValue { get; set; } = null;
        public Ordering orderBy { get; set; } = Ordering.Null;
        public int? skip { get; set; } = null;
        public int? Take { get; set; } = null;
        public DateTime? Start { get; set; } = null;
        public DateTime? End { get; set; } = null;
    }
}
