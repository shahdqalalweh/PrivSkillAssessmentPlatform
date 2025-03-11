using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Application.Services;
using SkillAssessmentPlatform.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> RegisterApplicant([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _authService.RegisterApplicantAsync(userRegisterDTO);

            if (result == "0")
            {
                return BadRequest(new { Message = "Invalid data." });
            }
            if ( result == "1")
            {
                return Ok(new { Message = "Succeded." });
            }
            return BadRequest(new { Message = result });
        }
        [HttpPost("register/examiner")]
       // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterExaminer(UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterExaminerAsync(userRegisterDTO);

            if (result == "0")
            {
                return BadRequest(new { Message = "Invalid data." });
            }
            if (result == "1")
            {
                return Ok(new { Message = "Succeded." });
            }
            return BadRequest(new { Message = result });
        }
  
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {

            var result = await _authService.LogInAsync(loginDTO);
            if (result == "0")
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(result);
        }
        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            if (email == null) return BadRequest();
            var result = await _authService.EmailConfirmation(email, token);
            if (result == "1")
            {
                return Redirect("http://localhost:5173/auth/login");
                
            }
            return BadRequest(result);
            //return Ok("Confirmation succeded");
            
        }
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            var result = await _authService.ForgotPasswordAsync(email);
            if ( result == "1")
            {
                return Ok("forgot email has sent");
            }
            return BadRequest(result);
        }
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            if (dto == null) return BadRequest();
            var result = await _authService.ResetPasswordAsync(dto);
            if (result == "1")
            {
                return Ok("Succeded");
            }
            return BadRequest(result);

        }
        [HttpPost("changepassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            if (dto == null) return BadRequest();
            var result = await _authService.ChangePasswordAsync(dto);
            if (result == "1")
            {
                return Ok("Password changed successfully.");
            }
            return BadRequest(result);

        }





    }

} 

        
