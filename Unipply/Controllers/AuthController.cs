using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using Unipply.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Unipply.Services;
using System.Threading.Tasks;

namespace Unipply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserDataService _userDataService;

        public AuthController(
            ILogger<AuthController> logger,
            IUserDataService userDataService)
        {
            _logger = logger;
            _userDataService = userDataService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            var user = await _userDataService.FindUserByEmailAsync(model.Email);
            if (user != null && user.Password == model.Password && user.Email == model.Email)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
               {
                        new Claim("UserID", Guid.NewGuid().ToString()),
                        new Claim("Email", model.Email)
               }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("unipplyRitaTanya")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            var user = await _userDataService.FindUserByEmailAsync(model.Email);
            if (user == null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserID", Guid.NewGuid().ToString()),
                        new Claim("UserName", model.UserName),
                        new Claim("Email", model.Email)
                }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("unipplyRitaTanya")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                await _userDataService.CreateAsync(new UserData
                {
                    Id = Guid.NewGuid(),
                    UserName = model.UserName,
                    Password = model.Password,
                    Email = model.Email,
                });

                return Ok(new { token });
            }
            else 
                return BadRequest(new { message = "User with this Email already exist." });

        }
    }
}
