using CleanArchMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));

            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.RegisterUser(userInfo.Email, userInfo.Password);
            if (result)
            {
                //return GenerateToken(userInfo);
                return Ok($"User {userInfo.Email} was create successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid create attempt");
                return BadRequest(ModelState);
            }
        }


        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

            if(result)
            {
                return GenerateToken(userInfo);
                //return Ok($"User {userInfo.Email} login successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            // declarações do usuario
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuvalor", "meu valor 2023"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir tempo de expiração
            var expiration = DateTime.UtcNow.AddMinutes(10);

            //gerar token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: _configuration["Jwt:Issuer"],
                //audiencia
                audience: _configuration["Jwt:Audience"],
                //claims
                claims: claims,
                //data de expiração
                expires: expiration,
                //assinatura digital
                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
