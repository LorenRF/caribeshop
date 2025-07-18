using CaribeShop.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaribeShop.Infrastructure.Persistence.Context
{
    /// <summary>
    /// Representa el contexto de base de datos para la aplicación CaribeShop.
    /// Contiene la definición de las entidades y su configuración mediante Fluent API.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración para el contexto.
        /// </summary>
        /// <param name="options">Opciones de DbContext (cadena de conexión, proveedor, etc.)</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Definición de las tablas del modelo
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<User> Users => Set<User>();

        /// <summary>
        /// Configura las entidades del modelo utilizando Fluent API.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tablas
            // Asignación explícita de los nombres de tabla
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<CartItem>().ToTable("CartItems");
            #endregion

            #region Claves primarias
            // Definición de claves primarias
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<CartItem>().HasKey(c => c.Id);
            #endregion

            #region Relaciones
            // Un producto puede estar en muchos ítems del carrito
            modelBuilder.Entity<Product>()
                .HasMany(p => p.CartItems)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId);
            #endregion

            #region Propiedades configuradas

            #region Product
            // Precio con precisión decimal para evitar redondeos inexactos
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // Valor por defecto para control de auditoría
            modelBuilder.Entity<Product>()
                .Property(p => p.createdBy)
                .HasDefaultValue("default");
            #endregion

            #region CartItem
            // Valor por defecto para el campo creadoPor
            modelBuilder.Entity<CartItem>()
                .Property(c => c.createdBy)
                .HasDefaultValue("default");
            #endregion

            #region User
            // Valor por defecto para el campo creadoPor
            modelBuilder.Entity<User>()
                .Property(u => u.createdBy)
                .HasDefaultValue("default");
            #endregion

            #endregion
        }
    }
}
