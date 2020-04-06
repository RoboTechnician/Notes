using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Notes.Models
{
    /// <summary>
    /// Full user class with auxiliary methods, inherit class with users's public data only
    /// </summary>
    public class User : PublicUser
    {
        /// <summary>
        /// Salt max length
        /// </summary>
        public const int SaltLength = 32;
        /// <summary>
        /// Salt byte length
        /// </summary>
        public const int SaltByteLength = 16;
        /// <summary>
        /// Password max length
        /// </summary>
        public const int PasswordLength = 64;
        /// <summary>
        /// Password min length
        /// </summary>
        public const int PasswordMinLength = 6;
        /// <summary>
        /// Crypted password max length
        /// </summary>
        public const int CryptPasswordLength = 16;
        /// <summary>
        /// Crypted password byte length
        /// </summary>
        public const int CryptPasswordByteLength = 32;

        public string Password { get; set; }
        public string Salt { get; set; }

        /// <summary>
        /// Create crypted password with random salt
        /// </summary>
        public void CreateCryptPassword(string password)
        {
            var byteSalt = new byte[SaltByteLength];
            var rngCsp = new RNGCryptoServiceProvider();

            rngCsp.GetBytes(byteSalt);
            Salt = Convert.ToBase64String(byteSalt);

            Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: byteSalt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: CryptPasswordByteLength));

        }

        /// <summary>
        /// Validate input password
        /// </summary>
        public bool ValidatePassword(string password)
        {
            var hashPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: CryptPasswordByteLength));

            return Password == hashPassword;
        }
    }
}
