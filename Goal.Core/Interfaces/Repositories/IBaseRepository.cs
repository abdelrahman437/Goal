using System.Linq.Expressions;
using Goal.Core.Common;
using Goal.Core.Helpers;
using Goal.Core.Specifications;

namespace Goal.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {

        Task<List<TResult>> GetAsync<TResult>(ISpecification<T> specification);
        Task<TResult> GetByIdAsync<TResult>(ISpecification<T> specification);
        Task AddAsync(T Entity);
        Task AddManyAsync(IEnumerable<T> Entities);
        Task Update(T Entity);
        Task UpdateMany(IEnumerable<T> Entities);
        Task Delete(T Entity);
        int count(ISpecification<T> specification);
        Task Restore(ISoftDeleteable softDeleteable);
        IQueryable<T> SearchAsync(int take,
                    Expression<Func<T, bool>> predicate);

    }
}
