using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Core.DTO;
using Goal.Core.Models;


namespace Goal.Core.Interfaces.Services
{
    public interface ICartItemServices
    {
        Task<List<CartItemDTO>> GetAll(int customerId);
        Task Add(CartItemDTO cartItem);
        Task Remove(int cartItemId, int customerId);
        Task Update(CartItemDTO cartItem);
    }
}
