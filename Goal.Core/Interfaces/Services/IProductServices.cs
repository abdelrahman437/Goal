using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Core.DTO;
using Goal.Core.Models;

namespace Goal.Core.Interfaces.Services
{
    public interface IProductServices
    {
        Task<List<ProductDTO>> GetAll(ProductQueryParameters productQueryParameters);
        Task<ProductDTO> GetById(int id);
        Task<List<ProductDTO>> Specials(int SizeOfPage, int NumberOfPage);
        Task Delete(int id);
        Task Restore(int id, int Quntity);
        Task Update(int id, UpdateProductDTO product);
        Task<List<SearchDTO>> Search(int take, string name);
        Task Add(AddProductDTO productDTO);


    }
}
