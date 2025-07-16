using Goal.Core.Interfaces.Services;
using Goal.Core.DTO;
using Goal.Core.UnitOfWork;
using Goal.Core.Specifications;
using Goal.Core.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;

namespace Goal.Data.Services
{
    public class CartItemServices : ICartItemServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartItemServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CartItemDTO>> GetAll(int customerId)
        {
            var specification = new DefaultSpecification<CartItem>()
            {
                predicate = p=>
                 p.CustomerId == customerId
            };
            var items = await _unitOfWork.CartItem.GetAsync<CartItemDTO>(specification);
            return items;
        }
        public async Task Add(CartItemDTO cartItemDTO)
        {
            CheackQuntity(cartItemDTO.Quntity);

            var cartItem = await GetByIdAsync(cartItemDTO.ProductId, cartItemDTO.CustomerId);


            if (cartItem != null)
            {
                throw new InvalidOperationException("Item Already in Cart");
            }

            
            _mapper.Map(cartItemDTO, cartItem);

            await _unitOfWork.CartItem.AddAsync(cartItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Remove(int cartItemId, int customerId)
        {
            var cartItem = await GetByIdAsync(cartItemId, customerId);

            if (cartItem == null)
                throw new KeyNotFoundException("Can't found this item");


            await _unitOfWork.CartItem.Delete(cartItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Update(CartItemDTO cartItemDTO)
        {
            CheackQuntity(cartItemDTO.Quntity);

            var item = await GetByIdAsync(cartItemDTO.ProductId, cartItemDTO.CustomerId);

            if (item == null)
                throw new KeyNotFoundException("Can't found this item");

            _mapper.Map(cartItemDTO, item);

            await _unitOfWork.CartItem.Update(item);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<CartItem> GetByIdAsync(int cartItemId, int customerId)
        {
            var specification = new DefaultSpecification<CartItem>()
            {
                predicate =
                  p => p.ProductId == cartItemId &&
                  p.CustomerId == customerId
            };
            var cartItem = await _unitOfWork.CartItem.GetByIdAsync<CartItem>(specification);
            return cartItem; 
        }
        private void CheackQuntity(int Quntity)
        {
            if (Quntity <= 0)
            {
                throw new ArgumentOutOfRangeException("The Quntity of Product can't be less than or equal 0");
            }
        }

    }
}
