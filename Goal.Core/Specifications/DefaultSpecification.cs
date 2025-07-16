using System.Linq.Expressions;

namespace Goal.Core.Specifications
{
    public class DefaultSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> predicate { get; set; }

        public List<Expression<Func<T, object>>> includes { get; set; }

        public Func<IQueryable<T>, IOrderedQueryable<T>> orderBy { get; set; }

        public bool AsTracking { get; set; }

        public int? Skip { get; set; }

        public int? Take { get; set; }

        public bool QueryFilter { get; set; }
    }
}
