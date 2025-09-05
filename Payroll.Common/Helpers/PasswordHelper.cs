using Microsoft.Extensions.Options;
using Payroll.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Common.Helpers
{
    //public static class PasswordHelper
    //{
    //    public static string HashPassword(string password)
    //    {
    //        byte[] salt = RandomNumberGenerator.GetBytes(16);
    //        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
    //        byte[] hash = pbkdf2.GetBytes(32);

    //        byte[] hashBytes = new byte[48];
    //        Array.Copy(salt, 0, hashBytes, 0, 16);
    //        Array.Copy(hash, 0, hashBytes, 16, 32);

    //        return Convert.ToBase64String(hashBytes);
    //    }

    //    public static bool VerifyPassword(string enteredPassword, string storedHash)
    //    {
    //        byte[] hashBytes = Convert.FromBase64String(storedHash);
    //        byte[] salt = new byte[16];
    //        Array.Copy(hashBytes, 0, salt, 0, 16);

    //        var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000, HashAlgorithmName.SHA256);
    //        byte[] hash = pbkdf2.GetBytes(32);

    //        for (int i = 0; i < 32; i++)
    //            if (hashBytes[i + 16] != hash[i])
    //                return false;

    //        return true;
    //    }
    //}


        public interface IPasswordHelper
        {
            string HashPassword(string password);
            bool VerifyPassword(string enteredPassword, string storedHash);
        }

        public class PasswordHelper : IPasswordHelper
        {
            private readonly int _workFactor;

            public PasswordHelper(IOptions<PasswordOptions> options)
            {
                _workFactor = options.Value.WorkFactor > 0 ? options.Value.WorkFactor : 12;
            }
      
    
            public string HashPassword(string password)
            {
                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("Password cannot be null or empty.", nameof(password));

                return BCrypt.Net.BCrypt.HashPassword(password, workFactor: _workFactor);
            }

            public bool VerifyPassword(string enteredPassword, string storedHash)
            {
                if (string.IsNullOrWhiteSpace(enteredPassword) || string.IsNullOrWhiteSpace(storedHash))
                    return false;

                return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
            }
        }
    }