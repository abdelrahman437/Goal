using AutoMapper;
using Goal.Core.Interfaces.Repositories;
using Goal.Core.Models;
using Goal.Core.UnitOfWork;
using Goal.Data.Repositories;

namespace Goal.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        public IProductRepository Product { get; private set; }
        public IBaseRepository<Discount> Discount { get; private set; }
        public IBaseRepository<Stock> Stock { get; private set; }

        public IBaseRepository<Category> Category { get; private set; }

        public IBaseRepository<Brand> Brand { get; private set; }
        public ICartItemRepository CartItem { get; private set; }
        public IBaseRepository<Image> Image { get; private set; }
        public UnitOfWork(AppDbContext context, IMapper _mapper)
        {
            _context = context;
            mapper = _mapper;

            Product = new ProductRepository(context,mapper);
            Discount = new BaseRepository<Discount>(context,mapper);
            Stock = new BaseRepository<Stock>(context, mapper);
            Category = new BaseRepository<Category>(context, mapper);
            Brand = new BaseRepository<Brand>(context, mapper);
            CartItem = new CartItemRepository(context, mapper);
            Image = new BaseRepository<Image>(context, mapper);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
