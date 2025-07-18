using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaribeShop.Core.Application.SaveViewModel
{
    /// <summary>
    /// Modelo para guardar datos de usuario durante el registro.
    /// </summary>
    public class UserSaveViewModel
    {
        public string userName { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string? address { get; set; } = string.Empty;
    }
}
