using Goal.Core.Interfaces.Repositories;
using Goal.Core.Models;

namespace Goal.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IBaseRepository<Discount> Discount { get; }
        IBaseRepository<Stock> Stock { get; }
        IBaseRepository<Category> Category { get; }
        IBaseRepository<Brand> Brand { get; }
        ICartItemRepository CartItem { get; }
        IBaseRepository<Image> Image { get; }
        Task<int> CompleteAsync();
        public bool HasChanges();
    }
}
