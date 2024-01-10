using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.PasswordManager
{
    public static class GenerateToken
    {
        public static string ReturnToken(string username, Guid userId, string userTypeName )
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("your_secret_key_here"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim("Username", username),
            new Claim("UserID", userId.ToString()),
            new Claim("UserType", userTypeName),

            // Add more claims as needed (e.g., user roles, etc.)
        };

            var token = new JwtSecurityToken(
                issuer: "your_issuer_here",
                audience: "your_audience_here",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Set token expiration time
                signingCredentials: credentials
            );
            var data = new JwtSecurityTokenHandler().WriteToken(token);

            return data;
        }
    }
}
