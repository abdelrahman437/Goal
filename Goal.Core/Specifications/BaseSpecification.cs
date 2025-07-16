using System.Linq.Expressions;


namespace Goal.Core.Specifications
{
    public abstract class BaseSpecification<T>:ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> predicate { get; protected set; }
        public List<Expression<Func<T, object>>> includes { get; } = new();
        public Func<IQueryable<T>, IOrderedQueryable<T>> orderBy { get; protected set; }
        public bool AsTracking { get; protected set; } = true;
        public int? Skip { get; protected set; }
        public int? Take { get; protected set; }
        public bool QueryFilter { get; protected set; } = true;
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
            => includes.Add(includeExpression);
        protected void ApplyOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderByExpression)
            => orderBy = orderByExpression;
        protected void AsNoTracking () => AsTracking = false;
        protected void ApplyPaging(int? skip, int? take)
        {
            Skip = skip;
            Take = take;
        }
        protected void IgnoreQueryFilter() => QueryFilter = false;
    }
}
