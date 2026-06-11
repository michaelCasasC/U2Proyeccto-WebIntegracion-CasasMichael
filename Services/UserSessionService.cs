using Microsoft.AspNetCore.Http;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Services
{
    public class UserSessionService : IUserSessionService
    {
        private const string SessionKeyUserId = "CurrentUserId";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public UserSessionService(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private ISession Session => _httpContextAccessor.HttpContext?.Session ?? throw new InvalidOperationException("Session is not available.");

        public int? GetCurrentUserId()
        {
            return Session.GetInt32(SessionKeyUserId);
        }

        public void SetCurrentUserId(int userId)
        {
            Session.SetInt32(SessionKeyUserId, userId);
        }

        public void Clear()
        {
            Session.Remove(SessionKeyUserId);
        }

        public bool IsLoggedIn => GetCurrentUserId().HasValue;

        public Usuario? GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return null;
            }

            return _context.Usuarios.Find(userId.Value);
        }
    }
}