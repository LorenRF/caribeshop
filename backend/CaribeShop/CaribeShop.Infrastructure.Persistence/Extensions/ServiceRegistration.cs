using CaribeShop.Core.Application.Interfaces.Repositories;
using CaribeShop.Core.Application.Interfaces.Services;
using CaribeShop.Core.Application.Services;
using CaribeShop.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CaribeShop.Infrastructure.Persistence.Extensions
{
    /// <summary>
    /// Clase de extensión para registrar todos los servicios y repositorios
    /// necesarios para la aplicación.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Método de extensión que registra las dependencias en el contenedor de servicios.
        /// Debe ser llamado desde Program.cs.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            #region Repositorios
            // Registra las implementaciones concretas de los repositorios
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Servicios
            // Registra los servicios de aplicación (lógica de negocio)
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Servicio auxiliar para generación de tokens JWT
            services.AddScoped<JwtTokenGenerator>();
            #endregion
        }
    }
}
