using CaribeShop.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaribeShop.Core.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartAsync(string sessionId);
        Task<CartItem?> GetItemAsync(string sessionId, int productId);
        Task AddItemAsync(CartItem item);
        Task UpdateItemAsync(CartItem item);
        Task RemoveItemAsync(int itemId);
        Task ClearCartAsync(string sessionId);
        Task SaveAsync();
    }
}
