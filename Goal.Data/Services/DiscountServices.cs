using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Goal.Core.DTO;
using Goal.Core.Helpers;
using Goal.Core.Interfaces.Services;
using Goal.Core.Models;
using Goal.Core.Specifications;
using Goal.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Goal.Data.Services
{
    public class DiscountServices : IDiscountServices
    {

        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public DiscountServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Add(DiscountDTO discount)
        {
            var Exception = discount.DiscountHelper();
            if (Exception.Erorr)
            {
                throw new Exception(Exception.massage);
            }
            Discount p = new Discount();
            _mapper.Map(discount, p);


            await _unitOfWork.Discount.AddAsync(p);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Delete(int id)
        {
            var discount = await GetById(id);

            await _unitOfWork.Discount.Delete(discount);
            await _unitOfWork.CompleteAsync();
        }
        public async Task Restore(int id)
        {
            var discount = await GetById(id);
            await _unitOfWork.Discount.Restore(discount);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<List<Discount>> GetAll(DiscountQueryParameters queryParameters)
        {
            DiscountSpecification specification = new DiscountSpecification(queryParameters);
            var discount = _unitOfWork.Discount.GetAsync<Discount>(specification);
            return await discount;
        }

        public async Task<Discount> GetById(int id, bool IgnoreQueryfilter = true)
        {
            var p = await _unitOfWork.Discount
                .GetByIdAsync<Discount>(new DefaultSpecification<Discount>
                {
                    predicate = p => p.Id == id,
                    AsTracking = true,
                    QueryFilter = !IgnoreQueryfilter

                });
            if (p == null)
                throw new KeyNotFoundException($"Don't Find Discount With Id {id}");
            return p;
        }

        public async Task<List<SearchDTO>> Search(int take, string name)
        {
            if (take < 0)
                take = 0;
            return await _unitOfWork.Discount
               .SearchAsync(take,
               p => p.Name.ToLower().Contains(name.ToLower()))
               .Select(p => new SearchDTO { id = p.Id, name = p.Name }).ToListAsync();
        }

        public async Task Update(int id, DiscountDTO discountDTO)
        {
            var discount = await GetById(id);

            var Exception = discountDTO.DiscountHelper();
            if (Exception.Erorr)
            {
                throw new Exception(Exception.massage);
            }

            _mapper.Map(discount, discountDTO);

            await _unitOfWork.Discount.Update(discount);

            await _unitOfWork.CompleteAsync();
        }
    }
}
