using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Notes.Models
{
    /// <summary>
    /// Token for jwt authentication
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Create token with user's data and secret key
        /// </summary>
        public TokenModel(User user, string key)
        {
            CreateToken(user, key);
        }

        public string Token { get; set; }

        /// <summary>
        /// Create token with user's data and secret key
        /// </summary>
        public void CreateToken(User user, string key)
        {
            var jwt = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                },
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256)
                );

            Token = new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
