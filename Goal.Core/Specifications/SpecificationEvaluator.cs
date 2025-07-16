using Microsoft.EntityFrameworkCore;

namespace Goal.Core.Specifications
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputquery,ISpecification<T> spec) {
            IQueryable<T> query = inputquery;
            if(spec.predicate != null)
                query = query.Where(spec.predicate);

            if (spec.includes != null)
                foreach (var include in spec.includes)
                    query = query.Include(include);
            if (spec.orderBy != null)
                query = spec.orderBy(query);
            if(spec.Skip.HasValue)
                query = query.Skip(spec.Skip.Value);
            if (spec.Take.HasValue)
                query = query.Take(spec.Take.Value);
            if(!spec.AsTracking)
                query = query.AsNoTracking();
            if (!spec.QueryFilter)
                query = query.IgnoreQueryFilters();
            return query;
        }
    }
}
