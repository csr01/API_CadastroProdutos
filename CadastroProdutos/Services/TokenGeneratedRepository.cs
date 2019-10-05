using CadastroProdutos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProdutos.Services
{
    public class TokenGeneratedRepository
    {

        private readonly IConfiguration _config;

        public TokenGeneratedRepository(IConfiguration config)
        {
            _config = config;
        }

        public Token Geratoken(LoginViewModel model)
        {
            var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub,model.Login),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Autentication:SecurityKey"]));
            int expirationTime = Convert.ToInt32(_config["Autentication:ExpiryInMinutes"]);

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Autentication:Issue"],
                audience: _config["Autentication:Audience"],
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(expirationTime)
            );

            var jwtToken = new Token
            {
                CurrentToken = new JwtSecurityTokenHandler().WriteToken(token),
                //RefreshToken = CreateRefreshToken(model.Login),
                ExpiresIn = expirationTime
            };

            return jwtToken;
        }

        //private string CreateRefreshToken(string login)
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        UserName = login,
        //        ExpirationDate = _settings.RefreshTokenExpiration
        //    };

        //    string token;
        //    var randomNumber = new byte[32];

        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomNumber);
        //        token = Convert.ToBase64String(randomNumber);
        //    }

        //    refreshToken.Token = token.Replace("+", string.Empty)
        //                .Replace("=", string.Empty)
        //                .Replace("/", string.Empty);

        //}
    }
}

