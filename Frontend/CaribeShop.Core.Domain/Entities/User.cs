using CaribeShop.Core.Domain.Common_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaribeShop.Core.Domain.Entities
{
    /// <summary>
    /// Representa la entidad User con propiedades básicas y de auditoría.
    /// </summary>
    public class User : AuditableBaseEntities
    {
        public string userName { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string passwordHash { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string? address { get; set; } = string.Empty;
        public string role { get; set; } = "User";
    }
}
