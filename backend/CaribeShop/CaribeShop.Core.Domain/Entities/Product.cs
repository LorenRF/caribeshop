using CaribeShop.Core.Domain.Common_Entities;

namespace CaribeShop.Core.Domain.Entities
{
    /// <summary>
    /// Representa la entidad Product con sus propiedades y relación a CartItems.
    /// </summary>
    public class Product : AuditableBaseEntities
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<CartItem> CartItems { get; set; }
    }
}
