using CaribeShop.Core.Application.Interfaces.Repositories;
using CaribeShop.Core.Application.ViewModel;
using CaribeShop.Core.Domain.Entities;
using CaribeShop.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace CaribeShop.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementación del repositorio de productos.
    /// Contiene la lógica para acceder a los datos de productos en la base de datos.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto de Entity Framework</param>
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los productos registrados en la base de datos.
        /// </summary>
        /// <returns>Lista de productos disponibles</returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
