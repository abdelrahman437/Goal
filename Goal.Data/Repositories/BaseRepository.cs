using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Goal.Core.Interfaces.Repositories;
using Goal.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Goal.Core.Specifications;
using System.Linq;
using Goal.Core.DTO;
using Goal.Core.Common;

namespace Goal.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public BaseRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TResult>> GetAsync<TResult>(ISpecification<T> specification)
        {
            IQueryable<T> _query = _context.Set<T>();
            return await SpecificationEvaluator<T>.GetQuery(inputquery: _query, specification).ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TResult> GetByIdAsync<TResult>(ISpecification<T> specification)
        {

            IQueryable<T> _query = _context.Set<T>();
            return await SpecificationEvaluator<T>.GetQuery(inputquery: _query, specification).ProjectTo<TResult>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }
        public Task Delete(T Entity)
        {            
            _context.Set<T>().Remove(Entity);
            return Task.CompletedTask;
        }


        public Task Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            return Task.CompletedTask;

        }
        public Task UpdateMany(IEnumerable<T> Entities)
        {
            _context.UpdateRange(Entities);
            return Task.CompletedTask;
        }

        public async Task AddAsync(T Entity)
        {
            await _context.AddAsync(Entity);
        }
        public async Task AddManyAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public int count(ISpecification<T> specification)
        {
            IQueryable<T> _query = _context.Set<T>();
            return SpecificationEvaluator<T>.GetQuery(inputquery: _query, specification).Count();
        }
        public Task Restore(ISoftDeleteable softDeleteable)
        {
            softDeleteable.UndoDelete();
            return Task.CompletedTask;
        }
        public IQueryable<T> SearchAsync(int take,
            Expression<Func<T, bool>> predicate)
        {
            var _query = _context.Set<T>().Where(predicate)
                .Take(take);
            return _query;
        }
    }
}