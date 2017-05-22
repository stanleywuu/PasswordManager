using System;
using System.IO;
using System.Security.Cryptography;

namespace Core
{
    public class AESEncryption
    {
        private byte[] IV;

        private byte[] CreateKey(string password, int keyBytes = 32)
        {
            byte[] salt = new byte[] { 80, 70, 60, 50, 40, 30, 20, 10 };
            int iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, salt, iterations);
            return keyGenerator.GetBytes(keyBytes);
        }

        public string Encrypt(string data, string key)
        {
           byte[] encrypted = AesEncryptStringToBytes(data, key);
           return Convert.ToBase64String(encrypted) + ";" + Convert.ToBase64String(IV);
        }

        private byte[] AesEncryptStringToBytes(string plaintext, string key)
        {
            byte[] encrypted;
            using (Aes aes = Aes.Create())
            {
                aes.Key = CreateKey(key);
                IV = aes.IV;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plaintext);
                    }
                    encrypted = memoryStream.ToArray();
                }
            }
            return encrypted;
        }
    }
}
