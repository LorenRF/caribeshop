using CaribeShop.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaribeShop.Controllers
{
    /// <summary>
    /// Controlador para manejar el carrito de compras.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Se recomienda usar un "SessionId" de prueba por ahora
        private string SessionId => "user-session-001";

        /// <summary>
        /// Obtiene el contenido actual del carrito.
        /// </summary>
        /// <returns>Contenido del carrito.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartService.GetCartAsync(SessionId);
            return Ok(cart);
        }

        /// <summary>
        /// Añade un ítem al carrito.
        /// </summary>
        /// <param name="productId">Id del producto.</param>
        /// <param name="quantity">Cantidad a añadir.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddItem(int productId, int quantity)
        {
            await _cartService.AddItemAsync(SessionId, productId, quantity);
            return Ok();
        }

        /// <summary>
        /// Actualiza la cantidad de un ítem en el carrito.
        /// </summary>
        /// <param name="productId">Id del producto.</param>
        /// <param name="quantity">Nueva cantidad.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateItem(int productId, int quantity)
        {
            await _cartService.UpdateItemQuantityAsync(SessionId, productId, quantity);
            return Ok();
        }

        /// <summary>
        /// Elimina un ítem del carrito.
        /// </summary>
        /// <param name="productId">Id del producto a eliminar.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            await _cartService.RemoveItemAsync(SessionId, productId);
            return Ok();
        }

        /// <summary>
        /// Vacía el carrito completamente.
        /// </summary>
        /// <returns>Resultado de la operación.</returns>
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync(SessionId);
            return Ok();
        }
    }
}
