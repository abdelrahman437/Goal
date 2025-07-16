using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Goal.Core.DTO;
using Goal.Core.Interfaces.Services;
using Goal.Core.Models;
using Goal.Core.Specifications;
using Goal.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Goal.Data.Services
{
    public class CategoryServices : ICategoryServices
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public CategoryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Add(CategoryDTO categoryDTO)
        {
            if(categoryDTO == null || categoryDTO.Name == null) 
                throw new NullReferenceException();
            Category category = new Category {
            Name = categoryDTO.Name,
            CreateAt = DateTime.Now,
            };
            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.CompleteAsync(); 
        }

        public async Task Delete(int id)
        {
            var category = await GetById(id);
            await _unitOfWork.Category.Delete(category);
            await _unitOfWork.CompleteAsync();
        }
        public async Task Restore(int id)
        {
            var category = await GetById(id);
            await _unitOfWork.Category.Restore(category);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<List<Category>> GetAll(string? name = null)
        {

            var category = await _unitOfWork.Category
                .GetAsync<Category>(new DefaultSpecification<Category>
                {
                    predicate = p => name != null ? p.Name.ToLower().Contains(name.ToLower()) : true
                });
            return category;
        }

        public async Task<Category> GetById(int id, bool IgnoreQueryfilter = true)
        {
            var category = await _unitOfWork.Category
               .GetByIdAsync<Category>(new DefaultSpecification<Category>
               {
                   predicate = p => p.Id == id,
                   AsTracking = true,
                   QueryFilter = !IgnoreQueryfilter

               });
            if (category == null)
                throw new KeyNotFoundException($"Don't Find Discount With Id {id}");
            return category;
        }

        public async Task<List<SearchDTO>> Search(int take, string name)
        {
            if (take < 0)
                take = 0;
            return await _unitOfWork.Category
               .SearchAsync(take,
               p => p.Name.ToLower().Contains(name.ToLower()))
               .Select(p => new SearchDTO { id = p.Id, name = p.Name }).ToListAsync();
        }

        public async Task Update(int id, CategoryDTO categoryDTO)
        {
            var category = await GetById(id);

            _mapper.Map(category, categoryDTO);

            await _unitOfWork.Category.Update(category);

            await _unitOfWork.CompleteAsync();
        }
    }
}
