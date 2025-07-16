using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Goal.Core.Interfaces.Repositories;
using Goal.Core.Models;

namespace Goal.Data.Repositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context,IMapper mapper) : base(context, mapper)
        {
        }
    }
}
