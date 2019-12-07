using TP2.Models.Entities;

namespace TP2.Services
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated { get; }
        User AuthenticatedUser { get; }
        void LogOut();
        void LogIn(string login, string password);
    }
}