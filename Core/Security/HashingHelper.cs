using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security
{
    public class HashingHelper
    {
        private static readonly string Salt = "sifra123";

        public static string CreateHash(string password)
        {
            using (var sha512 = SHA512.Create())
            {
                var saltedPassword = password + Salt;
                var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyHash(string checkingPassword, string hashedPassword)
        {
            var hashOfInput = CreateHash(checkingPassword);
            return hashOfInput == hashedPassword;
        }
    }
}
