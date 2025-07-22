using CaribeShop.Core.Application.Interfaces.Repositories;
using CaribeShop.Core.Domain.Entities;
using CaribeShop.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaribeShop.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementación del repositorio de carrito de compras.
    /// Administra las operaciones CRUD sobre los elementos del carrito en la base de datos.
    /// </summary>
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Contexto de EF Core</param>
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los productos en el carrito según la sesión del usuario.
        /// </summary>
        /// <param name="sessionId">Identificador único de sesión</param>
        /// <returns>Lista de elementos en el carrito</returns>
        public async Task<IEnumerable<CartItem>> GetCartAsync(string sessionId)
        {
            return await _context.CartItems
                .Include(ci => ci.Product) // Incluye los datos del producto asociado
                .Where(ci => ci.SessionId == sessionId)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un ítem específico del carrito por sesión y producto.
        /// </summary>
        public async Task<CartItem?> GetItemAsync(string sessionId, int productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.SessionId == sessionId && ci.ProductId == productId);
        }

        /// <summary>
        /// Agrega un nuevo ítem al carrito.
        /// </summary>
        public async Task AddItemAsync(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
        }

        /// <summary>
        /// Actualiza un ítem existente en el carrito.
        /// </summary>
        public Task UpdateItemAsync(CartItem item)
        {
            _context.CartItems.Update(item);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Elimina un ítem específico del carrito según su ID.
        /// </summary>
        public async Task RemoveItemAsync(int itemId)
        {
            var item = await _context.CartItems.FindAsync(itemId);
            if (item != null)
                _context.CartItems.Remove(item);
        }

        /// <summary>
        /// Vacía completamente el carrito de una sesión determinada.
        /// </summary>
        public async Task ClearCartAsync(string sessionId)
        {
            var items = _context.CartItems.Where(ci => ci.SessionId == sessionId);
            _context.CartItems.RemoveRange(items);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Guarda de forma persistente los cambios realizados en el carrito.
        /// </summary>
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
