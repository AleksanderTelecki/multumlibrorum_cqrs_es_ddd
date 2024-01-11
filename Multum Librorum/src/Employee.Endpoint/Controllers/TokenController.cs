using CQRS.Core.Queries.Abstract;
using Employee.Messages.Models;
using Employee.Messages.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Client.Messages.Models;
using Microsoft.AspNetCore.Authorization;

namespace Employee.Endpoint.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IQueryDispatcher _queryDispatcher;

        public TokenController(IConfiguration configuration, IQueryDispatcher queryDispatcher)
        {
            _configuration = configuration;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserToken(EmployeeCredentials emplCredentials)
        {
            if (emplCredentials != null && emplCredentials.Email != null && emplCredentials.Password != null)
            {
                var employee = await _queryDispatcher.Dispatch(new GetEmployeeByEmail { Email = emplCredentials.Email });

                if (employee != null && employee.Password == emplCredentials.Password)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("EmployeeId", employee.Id.ToString()),
                        new Claim("DisplayName", employee.Name),
                        new Claim("Role", employee.Role.ToString()),
                        new Claim("Email", employee.Email)
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
                        UserEmail = employee.Email,
                        UserName = employee.Name,
                        UserId = employee.Id
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
