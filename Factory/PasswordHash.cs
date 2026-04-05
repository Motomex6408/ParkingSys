using System;
using System.Security.Cryptography;

namespace Factory
{
    public class PasswordHash
    {
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] hash;

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, 16, 10000))
            {
                salt = pbkdf2.Salt;
                hash = pbkdf2.GetBytes(32);
            }

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string savedHash)
        {
            string[] parts = savedHash.Split(':');

            if (parts.Length != 2)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] testHash = pbkdf2.GetBytes(32);

                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != testHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}