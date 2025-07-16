using System.Linq.Expressions;


namespace Goal.Core.Specifications
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> predicate { get; }
        List<Expression<Func<T, object>>> includes { get; }
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy { get; }
        bool AsTracking { get; }
        int? Skip { get; }
        int? Take { get; }
        bool QueryFilter { get; }
    }
}