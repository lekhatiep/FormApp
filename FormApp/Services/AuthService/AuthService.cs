using FormApp.Dtos.UserDto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public AuthReponseDto GenerateToken(string userName, string email)
        {
            var SECRET_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JwtOptions:Key")));
            //Register configuration and validate token
            string issuer = _config["JwtOptions:Issuer"];
            string audience = _config["JwtOptions:Issuer"];
            string signingKey = _config.GetValue<string>("JwtOptions:Issuer");
            //string SECRET_KEY = _config.GetValue<string>("Token:Key");

            //Tao chung chi + kieu ma hoa cho chu ky 
            var credentials = new SigningCredentials(SECRET_KEY, SecurityAlgorithms.HmacSha256);

            //Token will be good for 1 minutes + refresh_token

            DateTime Expriry = DateTime.UtcNow.AddMinutes(9000);
            int ts = (int)(Expriry - new DateTime(1970, 1, 1)).TotalSeconds;
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, userName));
           // claims.Add(new Claim(ClaimTypes.Email, email));
           // claims.Add(new Claim("id", user.Id.ToString()));

            var secToken = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(9000),
                signingCredentials: credentials
            );

            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);//Sercurity Token

            return new AuthReponseDto
            {
                Token = tokenString,
                TokenType = "Bearer",
                TotalSecond = ts
            };
        }
    }
}
