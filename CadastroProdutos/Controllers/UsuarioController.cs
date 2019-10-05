using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CadastroProdutos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CadastroProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _config;

        public UsuarioController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IConfiguration config)
        {
            //var user = User.FindFirst("sub").Value; recupera o sub do token
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> CreateUsuario(RegisterViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario { UserName = usuario.Login };
                var result = await _userManager.CreateAsync(user, usuario.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(usuario);
                }
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Token(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password,true,true);
                if (result.Succeeded)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,model.Login),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, "Admin")
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

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }

                return Unauthorized();//401
            }
            return BadRequest();
        }


    }
}