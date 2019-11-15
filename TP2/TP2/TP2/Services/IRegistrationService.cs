using TP2.Models.Entities;

namespace TP2.Services
{
    public interface IRegistrationService
    {
        bool IsUserRegistered { get; }
        User RegisteredUser { get; }
        void RegisterUser(string login, string password);
    }
}