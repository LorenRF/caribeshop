using CaribeShop.Core.Domain.Common_Entities;

namespace CaribeShop.Core.Domain.Entities
{
    /// <summary>
    /// Representa un ítem dentro del carrito de compras.
    /// </summary>
    public class CartItem : AuditableBaseEntities
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public string SessionId { get; set; } = string.Empty;
    }
}
