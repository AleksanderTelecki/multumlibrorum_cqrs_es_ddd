using CQRS.Core.Queries.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Client.Messages.Models;
using Client.Messages.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Client.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController: ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IQueryDispatcher _queryDispatcher;

        public TokenController(IConfiguration configuration, IQueryDispatcher queryDispatcher)
        {
            _configuration = configuration;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserToken(ClientCredentials userCredentials)
        {
            if (userCredentials != null && userCredentials.Email != null && userCredentials.Password != null)
            {
                var user = await _queryDispatcher.Dispatch(new GetClientByEmailQuery { Email = userCredentials.Email });

                if (user != null && user.Password == userCredentials.Password)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("DisplayName", user.Name),
                        new Claim("Email", user.Email)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        null,
                        null,
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var tokenWithUserInfo = new TokenWithUserInfo()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        UserEmail = user.Email,
                        UserName = user.Name,
                        UserId = user.Id
                    };
                    
                    return Ok(tokenWithUserInfo);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        
        [Authorize]
        [HttpGet("Check")]
        public IActionResult CheckToken()
        {
            return Ok();
        }

    }
}
