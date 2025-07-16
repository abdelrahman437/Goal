using AutoMapper;
using AutoMapper.QueryableExtensions;
using Goal.Core.Interfaces.Repositories;
using Goal.Core.Models;
using Goal.Core.DTO;
using Microsoft.EntityFrameworkCore;
namespace Goal.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext context,IMapper mapper) : base(context, mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> SpecialAsync(int SizeOfPage, int NumberOfPage)
        {
            return await
                _db.products
                .Include(e => e.Images)
                .Include(e => e.Stock)
                .Include(e => e.Discount)
                .AsNoTracking().OrderByDescending(e => e.Stock.Sold)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .Skip((NumberOfPage - 1) * SizeOfPage)
                .Take(SizeOfPage)
                .ToListAsync();
        }



    }
}
