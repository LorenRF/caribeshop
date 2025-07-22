using CaribeShop.Core.Application.Helpers;
using CaribeShop.Core.Application.Interfaces.Repositories;
using CaribeShop.Core.Application.Interfaces.Services;
using CaribeShop.Core.Application.SaveViewModel;
using CaribeShop.Core.Application.ViewModel;
using CaribeShop.Core.Domain.Entities;

namespace CaribeShop.Core.Application.Services
{
    /// <summary>
    /// Servicio para la autenticación y registro de usuarios.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IUserRepository userRepo, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepo = userRepo;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="model">Datos del usuario a registrar.</param>
        /// <returns>False si el usuario ya existe, true si se registró correctamente.</returns>
        public async Task<bool> Register(UserSaveViewModel model)
        {
            if (await _userRepo.GetByUsernameAsync(model.userName) is not null)
                return false;

            var user = new User
            {
                userName = model.userName,
                name = model.name,
                lastName = model.lastName,
                email = model.email,
                passwordHash = PasswordEncryption.Hash(model.password),
                createdBy = model.userName,
                address = model.address ?? string.Empty
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();
            return true;
        }

        /// <summary>
        /// Valida credenciales y retorna un identificador de sesión.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <returns>Identificador de sesión o null si falla autenticación.</returns>
        public async Task<string?> Login(string username, string password)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user is null || !PasswordEncryption.Verify(password, user.passwordHash))
                return null;

            return $"session-{user.Id}";
        }

        /// <summary>
        /// Obtiene datos públicos del usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <returns>Modelo de usuario o null si no existe.</returns>
        public async Task<UserViewModel?> GetUserAsync(string username)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null) return null;

            return new UserViewModel
            {
                userName = user.userName,
                name = user.name,
                lastName = user.lastName,
                email = user.email
            };
        }

        /// <summary>
        /// Obtiene el hash de la contraseña del usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <returns>Hash de contraseña o null si no existe.</returns>
        public async Task<string?> GetPasswordAsync(string username)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            return user?.passwordHash;
        }
    }
}
