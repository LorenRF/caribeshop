using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CaribeShop.Core.Domain.Common_Entities
{
    /// <summary>
    /// Clase base con propiedades de auditoría para entidades.
    /// </summary>
    public class AuditableBaseEntities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { set; get; }

        public string createdBy { set; get; } = string.Empty;
        public DateTime created { set; get; } = DateTime.Now;
        public string? modifiedBy { set; get; }
        public DateTime? modified { set; get; }
    }
}
