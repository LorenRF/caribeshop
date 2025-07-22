using CaribeShop.Core.Application.Interfaces.Repositories;
using CaribeShop.Core.Application.Interfaces.Services;
using CaribeShop.Core.Application.ViewModel;

namespace CaribeShop.Core.Application.Services
{
    /// <summary>
    /// Servicio para operaciones relacionadas con productos.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los productos y los mapea a su ViewModel.
        /// </summary>
        /// <returns>Lista de productos en formato ProductViewModel.</returns>
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _repository.GetAllProductsAsync();

            return products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            });
        }
    }
}
