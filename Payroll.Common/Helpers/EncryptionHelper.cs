using Microsoft.Extensions.Options;
using Payroll.Common.Configurations;
using System.Security.Cryptography;
using System.Text;

namespace Payroll.Common.Helpers
{
    public interface IEncryptionHelper
    {
        byte[] EncryptStringToBytes(string plainText);
        string DecryptStringFromBytes(byte[] cipherBytes);
    }

    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly byte[] _key;

        public EncryptionHelper(IOptions<JwtSettings> jwtOptions)
        {
            if (jwtOptions?.Value is null || string.IsNullOrWhiteSpace(jwtOptions.Value.Key))
                throw new ArgumentNullException(nameof(jwtOptions), "JWT Key cannot be null or empty.");

            _key = Encoding.UTF8.GetBytes(jwtOptions.Value.Key);

            if (_key.Length != 16 && _key.Length != 24 && _key.Length != 32)
                throw new ArgumentException("JWT Key must be 16, 24, or 32 characters long.");
        }


        public byte[] EncryptStringToBytes(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return Array.Empty<byte>();

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
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

        public string DecryptStringFromBytes(byte[] cipherBytes)
        {
            if (cipherBytes is null || cipherBytes.Length == 0)
                return string.Empty;

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            int ivLength = aes.BlockSize / 8; 
            if (cipherBytes.Length < ivLength)
                return string.Empty;

            var iv = new byte[ivLength];
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