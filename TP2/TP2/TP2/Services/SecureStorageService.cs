using System;
using System.Threading.Tasks;
using TP2.Models.Entities;
using Xamarin.Essentials;

namespace TP2.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        public SecureStorageService()
        {

        }

        public async Task<string> GetUserEncryptionKeyAsync(User user)
        {
            return await SecureStorage.GetAsync(user.Login);
        }

        public async Task SetUserEncryptionKeyAsync(User user, string encryptionKey)
        {
            await SecureStorage.SetAsync(user.Login, encryptionKey);
        }
    }
}