using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SeguridadCiudadana.Domain.Entities;
using SC.Infrastructure.Repositories;

namespace BlazorCRUD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public CuentasController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

      /*  [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] Tipousuario tipousuario)
        {
            var user = new IdentityUser { UserName = tipousuario.Correo, Email = tipousuario.Contraseña };
            var result = await _userManager.CreateAsync(user, tipousuario.Contraseña);
            if (result.Succeeded)
            {
                return BuildToken(tipousuario);
            }
            else
            {
                return BadRequest("Username or password invalid");
            }

        }*/

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] Tipousuario tipousuario)
        {
            var _context = new RepoSql();
        
            var respuesta = await _context.GetUser(tipousuario);
            var respuesta2 = respuesta.Idcargo.ToString();

          

            
            //var result = await _signInManager.(tipousuario.Correo, tipousuario.Contraseña, isPersistent: false, lockoutOnFailure: false);
            if (respuesta!=null)
            {
                return BuildToken(tipousuario, respuesta2);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken BuildToken(Tipousuario tipousuario, string cargo)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.UniqueName, tipousuario.Correo),
        new Claim(ClaimTypes.Name, tipousuario.Correo),
        new Claim(ClaimTypes.Role, cargo),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           
            var issuerr =  "1";
            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            var expiration = DateTime.UtcNow.AddYears(1);
            
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Rol = cargo,
                Expiration = expiration
            };
        }
    }
}
