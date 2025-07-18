using CaribeShop.Core.Application.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CaribeShop.Infrastructure.Persistence.Extensions
{
    /// <summary>
    /// Clase de extensión para configurar la autenticación JWT.
    /// Permite mover la lógica de configuración fuera del Program.cs.
    /// </summary>
    public static class AuthenticationServiceExtension
    {
        /// <summary>
        /// Registra y configura la autenticación JWT para proteger los endpoints de la API.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación</param>
        /// <param name="configuration">Configuración general de la aplicación (appsettings)</param>
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Lee la sección JwtSettings del archivo appsettings.json
            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            // Extrae las configuraciones necesarias
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            // Agrega el servicio de autenticación con el esquema JWT Bearer
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // útil en desarrollo local
                options.SaveToken = true;

                // Configura los parámetros de validación del token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // evita tolerancias innecesarias en expiración
                };
            });
        }
    }
}
