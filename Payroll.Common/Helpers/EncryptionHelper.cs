using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");

        public static byte[] EncryptStringToBytes(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return Array.Empty<byte>();

            using var aes = Aes.Create();
            aes.Key = Key;
            aes.GenerateIV();

            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return ms.ToArray();
        }

        public static string DecryptStringFromBytes(byte[] cipherBytes)
        {
            if (cipherBytes == null || cipherBytes.Length == 0)
                return string.Empty;

            using var aes = Aes.Create();
            aes.Key = Key;

            int ivLength = aes.BlockSize / 8;
            if (cipherBytes.Length < ivLength)
                return string.Empty;

            byte[] iv = new byte[ivLength];
            Array.Copy(cipherBytes, 0, iv, 0, ivLength);
            aes.IV = iv;

            int cipherTextStartIndex = ivLength;
            int cipherTextLength = cipherBytes.Length - cipherTextStartIndex;

            using var ms = new MemoryStream(cipherBytes, cipherTextStartIndex, cipherTextLength);
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
