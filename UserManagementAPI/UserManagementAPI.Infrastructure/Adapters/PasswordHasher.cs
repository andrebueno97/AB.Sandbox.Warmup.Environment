namespace UserManagementAPI.Infrastructure.Adapters;

using System.Security.Cryptography;
using UserManagementAPI.Domain.Ports;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int Iterations = 10000;
    private const int KeySize = 32;

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var hash = pbkdf2.GetBytes(KeySize);

        var hashBytes = new byte[SaltSize + KeySize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool Verify(string password, string hash)
    {
        var hashBytes = Convert.FromBase64String(hash);
        
        if (hashBytes.Length != SaltSize + KeySize)
            return false;

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
        var hashOfInput = pbkdf2.GetBytes(KeySize);

        return CryptographicOperations.FixedTimeEquals(hashOfInput, hashBytes.AsSpan(SaltSize, KeySize));
    }
}
