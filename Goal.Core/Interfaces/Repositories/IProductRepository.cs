using Goal.Core.Models;
using Goal.Core.DTO;

namespace Goal.Core.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<List<ProductDTO>> SpecialAsync(int SizeOfPage, int NumberOfPage);
    }
}
