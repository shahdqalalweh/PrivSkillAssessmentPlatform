using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Application.Services;
using SkillAssessmentPlatform.Core.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SkillAssessmentPlatform.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;


        public AuthController( AuthService authService)
        {

            _authService = authService;
        }
        [HttpPost("register/applicant")]
        public async Task<IActionResult> RegisterApplicant([FromBody] UserRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _authService.RegisterApplicant(model, model.Password);
            /// need to enhance based on returnd value
            if (result == "Succeeded")
            {
                return Ok(new { Message = "Applicant registered successfully." });
            }
            return BadRequest(new { Message = result });
        }
        [HttpPost("register/examiner")]
        public async Task<IActionResult> RegisterExaminer(UserRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authService.RegisterExaminer(model, model.Password);
            /// need to enhance based on returnd value
            return Ok(result);
        }
        /*
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign role to user
                await _userManager.AddToRoleAsync(user, model.Role);

                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:securityKey"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:validIssuer"],
                    audience: _configuration["JwtSettings:validAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }
  */
    } 
}
        
