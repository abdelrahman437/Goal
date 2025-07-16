using Goal.Core.DTO;
using Goal.Core.Models;

namespace Goal.Core.Interfaces.Services
{
    public interface IBrandServices
    {
        Task<List<Brand>> GetAll(string? name = null);
        Task<Brand> GetById(int id,bool IgnoreQueryfilter = true);
        Task Update(int id, BrandDTO discount);
        Task Add(BrandDTO discount);
        Task<List<SearchDTO>> Search(int take, string name);
        Task Delete(int id);
        Task Restore(int id);
    }
}
