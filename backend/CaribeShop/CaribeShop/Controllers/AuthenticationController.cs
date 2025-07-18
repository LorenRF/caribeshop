using CaribeShop.Core.Application.Helpers;
using CaribeShop.Core.Application.Interfaces.Services;
using CaribeShop.Core.Application.SaveViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CaribeShop.Controllers
{
    /// <summary>
    /// Controlador para autenticación y registro de usuarios.
    /// </summary>
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationController(IAuthenticationService authService, JwtTokenGenerator jwtTokenGenerator)
        {
            _authService = authService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="model">Datos del usuario.</param>
        /// <returns>Resultado del registro.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserSaveViewModel model)
        {
            var result = await _authService.Register(model);
            if (!result) return BadRequest("Username already exists.");
            return Ok(new { message = "User registered." });
        }

        /// <summary>
        /// Inicia sesión y genera un token JWT.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <returns>Token JWT o null si falla autenticación.</returns>
        [HttpPost("login")]
        public async Task<string?> Login(string username, string password)
        {
            var user = await _authService.GetUserAsync(username);
            var userPassword = await _authService.GetPasswordAsync(username);
            if (userPassword is null || !PasswordEncryption.Verify(password, userPassword))
                return null;

            return _jwtTokenGenerator.GenerateToken(user.userName);
        }

        /// <summary>
        /// Obtiene información pública de un usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <returns>Datos del usuario o NotFound si no existe.</returns>
        [HttpGet("user")]
        public async Task<IActionResult> GetUserInfo([FromQuery] string username)
        {
            var user = await _authService.GetUserAsync(username);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
