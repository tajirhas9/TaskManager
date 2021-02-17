using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManager.API.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using CryptoHelper;
using TaskManager.API.Services.Abstract;

namespace TaskManager.API.Services {
    public class AuthService : IAuthService {
        string JWTSecret;
        int JWTLifespan;
        public AuthService(string jwtSecret, int jwtLifespan) {
            this.JWTSecret = jwtSecret;
            this.JWTLifespan = jwtLifespan;
        }

        public AuthData GetAuthData(string id) {
            var expirationTime = DateTime.UtcNow.AddSeconds(JWTLifespan);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, id)
                }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSecret)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return new AuthData {
                Token = token,
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                Id = id
            };
        }

        public string HashPassword(string password) {
            return Crypto.HashPassword(password);
        }

        public bool VerifyPassword(string actualPassword, string hashedPassword) {
            return Crypto.VerifyHashedPassword(hashedPassword, actualPassword);
        }
    }
}
