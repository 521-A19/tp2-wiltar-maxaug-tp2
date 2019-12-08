namespace TP2.Services
{
    public interface ICryptoService
    {
        string Decrypt(string encryptedValue, string encryptionKey);
        string Encrypt(string clearValue, string encryptionKey);
        string GenerateSalt();
        string HashSHA512(string value, string salt);

        string GenerateEncryptionKey();
    }
}