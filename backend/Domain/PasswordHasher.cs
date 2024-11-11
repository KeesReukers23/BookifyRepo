using System.Security.Cryptography;
using System.Text;

namespace Logic
{
    public static class PasswordHasher
    {
        public static string HashPassword(string input)
        {
            // Gebruik de statische HashData methode
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));

            // Converteer de bytes naar een hexadecimale string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
