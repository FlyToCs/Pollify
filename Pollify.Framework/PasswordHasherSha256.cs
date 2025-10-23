namespace Pollify.Framework;

using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHasherSha256
{
    private const int SaltSize = 16;

    public static string HashPassword(string password)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));

        var salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var hash = ComputeSha256Hash(salt, password);

        var saltB64 = Convert.ToBase64String(salt);
        var hashB64 = Convert.ToBase64String(hash);

        return $"{saltB64}:{hashB64}";
    }


    public static bool VerifyPassword(string password, string storedSaltAndHash)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));
        if (string.IsNullOrWhiteSpace(storedSaltAndHash)) return false;

        var parts = storedSaltAndHash.Split(':');
        if (parts.Length != 2) return false;

        byte[] salt, storedHash;
        try
        {
            salt = Convert.FromBase64String(parts[0]);
            storedHash = Convert.FromBase64String(parts[1]);
        }
        catch
        {
            return false; 
        }

        var computedHash = ComputeSha256Hash(salt, password);
        return FixedTimeEquals(storedHash, computedHash);
    }


    private static byte[] ComputeSha256Hash(byte[] salt, string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var combined = new byte[salt.Length + passwordBytes.Length];
        Buffer.BlockCopy(salt, 0, combined, 0, salt.Length);
        Buffer.BlockCopy(passwordBytes, 0, combined, salt.Length, passwordBytes.Length);

        using (var sha = SHA256.Create())
        {
            return sha.ComputeHash(combined);
        }
    }

    private static bool FixedTimeEquals(byte[] a, byte[] b)
    {
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;

        int diff = 0;
        for (int i = 0; i < a.Length; i++)
        {
            diff |= a[i] ^ b[i];
        }
        return diff == 0;
    }
}
