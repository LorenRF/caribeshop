using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaribeShop.Core.Application.Helpers
{
    /// <summary>
    /// Proporciona métodos para encriptar y verificar contraseñas.
    /// </summary>
    public static class PasswordEncryption
    {
        /// <summary>
        /// Genera un hash SHA256 de una contraseña.
        /// </summary>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>Hash codificado en Base64.</returns>
        public static string Hash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verifica si una contraseña coincide con un hash dado.
        /// </summary>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <param name="hashedPassword">Hash a comparar.</param>
        /// <returns>True si coinciden, false en caso contrario.</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            return Hash(password) == hashedPassword;
        }
    }
}
