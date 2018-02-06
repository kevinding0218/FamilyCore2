using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Persistent.User
{
    public class PasswordRepository : IPasswordRepository
    {
        public String Encrypt(string password)
        {
            SHA512CryptoServiceProvider x = new SHA512CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = x.ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data).Replace("'", "|").Replace(";", "|").Replace("--", "|").Replace(",", "").Trim();
        }

        public String CreatePassword(int maxSize = 8)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
