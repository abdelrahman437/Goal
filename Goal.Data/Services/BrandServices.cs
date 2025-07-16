using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Goal.Core.DTO;
using Goal.Core.Interfaces.Services;
using Goal.Core.Models;
using Goal.Core.Specifications;
using Goal.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Goal.Data.Services
{
    public class BrandServices : IBrandServices
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BrandServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Add(BrandDTO brandDTO)
        {
            if (brandDTO == null || brandDTO.Name == null)
                throw new NullReferenceException(nameof(brandDTO));
            Brand brand = new Brand
            {
                Name = brandDTO.Name,
                CreateAt = DateTime.Now,
            };
            await _unitOfWork.Brand.AddAsync(brand);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Delete(int id)
        {
            var brand = await GetById(id);
            await _unitOfWork.Brand.Delete(brand);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<Brand>> GetAll(string? name = null)
        {

            var brand = await _unitOfWork.Brand
                .GetAsync<Brand>(new DefaultSpecification<Brand>
                {
                    predicate = p => name != null ? p.Name.ToLower().Contains(name.ToLower()) : true
                });
            return brand;
        }

        public async Task<Brand> GetById(int id,bool IgnoreQueryfilter = true)
        {
            var brand = await _unitOfWork.Brand
                .GetByIdAsync<Brand>(new DefaultSpecification<Brand>
                {
                    predicate = p=>p.Id == id,
                    AsTracking = true,
                    QueryFilter = !IgnoreQueryfilter

                });

            if (brand == null)
                throw new KeyNotFoundException($"Don't Find Brand With Id {id}");
            return brand;
        }

        public async Task Restore(int id)
        {
            var brand = await GetById(id);
            await _unitOfWork.Brand.Restore(brand);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<SearchDTO>> Search(int take, string name)
        {
            if (take < 0)
                take = 0;
            return await _unitOfWork.Brand
               .SearchAsync(take,
               p => p.Name.ToLower().Contains(name.ToLower()))
               .Select(p => new SearchDTO { id = p.Id, name = p.Name }).ToListAsync();
        }

        public async Task Update(int id, BrandDTO brandDTO)
        {
            var brand = await GetById(id);

            _mapper.Map(brand, brandDTO);

            await _unitOfWork.Brand.Update(brand);

            await _unitOfWork.CompleteAsync();
        }
    }
}
