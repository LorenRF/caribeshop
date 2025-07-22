using CaribeShop.Core.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaribeShop.Core.Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<CartContentsViewModel> GetCartAsync(string sessionId);
        Task AddItemAsync(string sessionId, int productId, int quantity);
        Task UpdateItemQuantityAsync(string sessionId, int productId, int quantity);
        Task RemoveItemAsync(string sessionId, int productId);
        Task ClearCartAsync(string sessionId);
    }
}
