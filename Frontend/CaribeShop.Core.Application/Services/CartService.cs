using CaribeShop.Core.Application.Interfaces.Repositories;
using CaribeShop.Core.Application.Interfaces.Services;
using CaribeShop.Core.Application.ViewModel;
using CaribeShop.Core.Domain.Entities;

namespace CaribeShop.Core.Application.Services
{
    /// <summary>
    /// Servicio para manejar la lógica del carrito de compras.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public CartService(ICartRepository cartRepo, IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        /// <summary>
        /// Obtiene el contenido del carrito para una sesión dada.
        /// </summary>
        /// <param name="sessionId">Identificador de sesión.</param>
        /// <returns>Modelo con los ítems del carrito.</returns>
        public async Task<CartContentsViewModel> GetCartAsync(string sessionId)
        {
            var items = await _cartRepo.GetCartAsync(sessionId);

            var viewModels = items.Select(ci => new CartItemViewModel
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                UnitPrice = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList();

            return new CartContentsViewModel { Items = viewModels };
        }

        /// <summary>
        /// Añade un ítem al carrito o incrementa la cantidad si ya existe.
        /// </summary>
        /// <param name="sessionId">Identificador de sesión.</param>
        /// <param name="productId">Id del producto.</param>
        /// <param name="quantity">Cantidad a añadir.</param>
        public async Task AddItemAsync(string sessionId, int productId, int quantity)
        {
            var existing = await _cartRepo.GetItemAsync(sessionId, productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
                await _cartRepo.UpdateItemAsync(existing);
            }
            else
            {
                var item = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    SessionId = sessionId
                };
                await _cartRepo.AddItemAsync(item);
            }

            await _cartRepo.SaveAsync();
        }

        /// <summary>
        /// Actualiza la cantidad de un ítem en el carrito.
        /// </summary>
        /// <param name="sessionId">Identificador de sesión.</param>
        /// <param name="productId">Id del producto.</param>
        /// <param name="quantity">Nueva cantidad.</param>
        public async Task UpdateItemQuantityAsync(string sessionId, int productId, int quantity)
        {
            var item = await _cartRepo.GetItemAsync(sessionId, productId);
            if (item != null)
            {
                item.Quantity = quantity;
                await _cartRepo.UpdateItemAsync(item);
                await _cartRepo.SaveAsync();
            }
        }

        /// <summary>
        /// Elimina un ítem del carrito.
        /// </summary>
        /// <param name="sessionId">Identificador de sesión.</param>
        /// <param name="productId">Id del producto a eliminar.</param>
        public async Task RemoveItemAsync(string sessionId, int productId)
        {
            var item = await _cartRepo.GetItemAsync(sessionId, productId);
            if (item != null)
            {
                await _cartRepo.RemoveItemAsync(item.Id);
                await _cartRepo.SaveAsync();
            }
        }

        /// <summary>
        /// Vacía todo el carrito para una sesión.
        /// </summary>
        /// <param name="sessionId">Identificador de sesión.</param>
        public async Task ClearCartAsync(string sessionId)
        {
            await _cartRepo.ClearCartAsync(sessionId);
            await _cartRepo.SaveAsync();
        }
    }
}
