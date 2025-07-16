using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Core.DTO;
using Goal.Core.Models;

namespace Goal.Core.Interfaces.Services
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetAll(string? name = null);
        Task<Category> GetById(int id, bool IgnoreQueryfilter = true);
        Task Update(int id, CategoryDTO categoryDTO);
        Task Add(CategoryDTO categoryDTO);
        Task<List<SearchDTO>> Search(int take, string name);
        Task Restore(int id);
        Task Delete(int id);
    }
}
