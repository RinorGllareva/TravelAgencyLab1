using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Services
{
    public class UserToken
    {
        private readonly IConfiguration _config;

        public UserToken(IConfiguration config)
        {
            _config = config;
        }
        public string CreateToken(User userr)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userr.Email),
                new Claim(ClaimTypes.Role, userr.Roli)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
