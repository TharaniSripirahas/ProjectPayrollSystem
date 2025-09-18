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