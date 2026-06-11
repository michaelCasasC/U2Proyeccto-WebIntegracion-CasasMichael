using Proyecto.Models;

namespace Proyecto.Services
{
    public interface IUserSessionService
    {
        int? GetCurrentUserId();
        void SetCurrentUserId(int userId);
        void Clear();
        bool IsLoggedIn { get; }
        Usuario? GetCurrentUser();
    }
}