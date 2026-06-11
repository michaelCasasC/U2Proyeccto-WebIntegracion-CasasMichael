using Proyecto.Models;
using Proyecto.ViewModels;

namespace Proyecto.Services
{
    public interface IUserService
    {
        Task<Usuario?> AuthenticateAsync(string email, string password);
        Task<Usuario> CreateUserAsync(RegisterViewModel model);
        Task<Usuario?> GetByIdAsync(int id);
        Task<bool> EmailExistsAsync(string email);
    }
}