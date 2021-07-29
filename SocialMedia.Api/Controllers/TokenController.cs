
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly ISecurityService _securityService;
        private readonly IPasswordService _passwordService; 
        public TokenController(IConfiguration configuration,ISecurityService securityService, IPasswordService passwordService)
        {
            _Configuration = configuration;
            _securityService = securityService;
            _passwordService = passwordService;
        }
        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            // is it u valid user
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }

            return NotFound("Usurio no registrado");
        }
        private async Task<(bool, Security )> IsValidUser(UserLogin login)
        {
            var user = await _securityService.GetLoginByCredentials(login);
            var isvalid = _passwordService.Check(user.Contrasena, login.Password);//validar contraseña

            return (isvalid, user);
        }

        private string GenerateToken(Security security)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Authentication:Secretkey"]));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            // claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, security.NombreUsuario),
                new Claim("User", security.Usuario),
                new Claim(ClaimTypes.Role, security.Rol.ToString())
            };
            // payload
            var payload = new JwtPayload(
                   _Configuration["Authentification:Issuer"],
                   _Configuration["Authentification:Audience"],
                   claims,
                   DateTime.Now,
                   DateTime.UtcNow.AddDays(1)
                );
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
