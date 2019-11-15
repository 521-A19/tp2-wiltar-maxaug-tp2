using TP2.Models.Entities;
using System.Threading.Tasks;

namespace TP2.Services
{
    public interface ISecureStorageService
    {
        Task<string> GetUserEncryptionKeyAsync(User user);
        Task SetUserEncryptionKeyAsync(User user, string encryptionKey);
    }
}