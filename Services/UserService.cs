using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;
using Proyecto.ViewModels;

namespace Proyecto.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UserService(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<Usuario?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            PasswordVerificationResult result;
            try
            {
                result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash ?? string.Empty, password);
            }
            catch (FormatException)
            {
                return null;
            }

            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<Usuario> CreateUserAsync(RegisterViewModel model)
        {
            var existing = await _context.Usuarios.AnyAsync(u => u.Email == model.Email);
            if (existing)
            {
                throw new InvalidOperationException("El correo electrónico ya está registrado.");
            }

            var user = new Usuario
            {
                Nombre = model.Nombre,
                Email = model.Email,
                Telefono = model.Telefono,
                EsCliente = model.EsCliente,
                EsEmprendedor = model.EsEmprendedor,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }
    }
}
