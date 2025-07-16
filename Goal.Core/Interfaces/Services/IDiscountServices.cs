using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Core.DTO;
using Goal.Core.Models;
using Goal.Core.Specifications;

namespace Goal.Core.Interfaces.Services
{
    public interface IDiscountServices
    {
        Task<List<Discount>> GetAll(DiscountQueryParameters queryParameters);
        Task<Discount> GetById(int id, bool IgnoreQueryfilter = true);
        Task Update(int id,DiscountDTO discount);
        Task Add(DiscountDTO discount);
        Task<List<SearchDTO>> Search(int take, string name);
        Task Restore(int id);
        Task Delete(int id);
    }
}
