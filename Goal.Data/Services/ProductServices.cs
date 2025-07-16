using System.Linq.Expressions;
using System.Reflection.Metadata;
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
    public class ProductServices:IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageServices _imageServices;
        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper, IImageServices imageServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageServices = imageServices;
        }


        public async Task Add(AddProductDTO productDTO)
        {
            Product product = new Product();
            _mapper.Map(productDTO, product);
            product.Images = new List<Image>();
            foreach (var image in productDTO.formFiles)
            {
                var photo = await _imageServices.UploadImageAsync(image);
                if (photo.Error != null)
                    throw new Exception(photo.Error.Message);
                product.Images.Add(new Image
                {
                    PublicId = photo.PublicId,
                    Path = photo.SecureUrl.ToString()
                });
            }

            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Delete(int id)
        {
            ProductSpecification single = new ProductSpecification(id, true, false, p => p.Stock);
            var product = await _unitOfWork.Product.GetByIdAsync<Product>(single);

            if (product == null)
                throw new KeyNotFoundException($"No Item With Id {id}");

            await _unitOfWork.Product.Delete(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<ProductDTO>> GetAll(ProductQueryParameters productQueryParameters)
        {
            ProductSpecification productSpecification = new ProductSpecification(productQueryParameters);
            var Products = await _unitOfWork.Product.GetAsync<ProductDTO>(productSpecification);
            return Products;
        }
        public async Task<Product> GetById(int id,params Expression<Func<Product, object>>[] include)
        {
            ProductSpecification single = new ProductSpecification(id, true, false, include);
            var product = await _unitOfWork.Product.GetByIdAsync<Product>(single);

            if (product == null)
                throw new KeyNotFoundException($"No Item With Id {id}");
            return product;
        }
        public async Task<ProductDTO> GetById(int id)
        {
            ProductSpecification single = new ProductSpecification(id, true, false, e => e.Stock);
            var product = await _unitOfWork.Product.GetByIdAsync<ProductDTO>(single);
            if (product == null)
                throw new KeyNotFoundException($"No Item With Id {id}");
            return product;
        }

        public async Task Restore(int id, int Quntity)
        {
            var product = await GetById(id, e=>e.Stock);

            await _unitOfWork.Product.Restore(product);

            if (Quntity > 0)
                product.Stock.Quantity = Quntity;

            else if (product.Stock.Quantity == 0)
                throw new Exception("the product still deleted");

            await _unitOfWork.Product.Update(product);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<SearchDTO>> Search(int take, string name)
        {
            if (take < 0 )
                take = 0;
            return await _unitOfWork.Product
               .SearchAsync(take,
               p => p.Name.ToLower().Contains(name.ToLower()))
               .Select(p => new SearchDTO { id = p.Id, name = p.Name }).ToListAsync();
        }

        public Task<List<ProductDTO>> Specials(int SizeOfPage, int NumberOfPage)
        {
            if (NumberOfPage <= 0 || SizeOfPage <= 0)
                throw new ArgumentOutOfRangeException("Invalid Size or Number Of Page ");

            return _unitOfWork.Product.SpecialAsync(SizeOfPage, NumberOfPage);
              
        }

        public async Task Update(int id, UpdateProductDTO productDTO)
        {
            var product = await GetById(id, e => e.Stock,equals=>equals.Images);

            _mapper.Map(productDTO, product);
            if (!_unitOfWork.HasChanges())
                throw new Exception("No changes");
            await _unitOfWork.Product.Update(product);
            await _unitOfWork.CompleteAsync();

        }
    }
}
