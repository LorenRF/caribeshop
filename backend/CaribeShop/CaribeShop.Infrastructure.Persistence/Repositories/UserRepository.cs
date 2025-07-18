using CaribeShop.Core.Application.Interfaces.Repositories;
using CaribeShop.Core.Domain.Entities;
using CaribeShop.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CaribeShop.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementación concreta del repositorio de usuarios.
    /// Se encarga de interactuar directamente con la base de datos.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos por inyección de dependencias.
        /// </summary>
        /// <param name="context">Contexto de la base de datos</param>
        public UserRepository(AppDbContext context) => _context = context;

        /// <summary>
        /// Busca un usuario en la base de datos por su nombre de usuario.
        /// </summary>
        /// <param name="username">Nombre de usuario a buscar</param>
        /// <returns>Usuario si existe; null en caso contrario</returns>
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.userName == username);
        }

        /// <summary>
        /// Agrega un nuevo usuario a la base de datos. No guarda aún los cambios.
        /// </summary>
        /// <param name="user">Entidad de usuario a agregar</param>
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        /// <summary>
        /// Guarda de forma persistente los cambios realizados en el contexto.
        /// </summary>
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
