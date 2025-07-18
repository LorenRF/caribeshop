using CaribeShop.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaribeShop.Controllers
{
    /// <summary>
    /// Controlador API para operaciones relacionadas con productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Obtiene un producto por su id.
        /// </summary>
        /// <param name="id">Id del producto.</param>
        /// <returns>Producto o NotFound si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _service.GetAllAsync();
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
    }
}
