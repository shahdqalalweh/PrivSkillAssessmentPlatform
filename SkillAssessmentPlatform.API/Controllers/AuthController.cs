using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.Tokens;
using SkillAssessmentPlatform.API.Common;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Application.Services;
using SkillAssessmentPlatform.Core.Common;
using SkillAssessmentPlatform.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace SkillAssessmentPlatform.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;
        private readonly IResponseHandler _responseHandler;

        public AuthController( AuthService authService, IResponseHandler  responseHandler)
        {

            _authService = authService;
            _responseHandler = responseHandler;
        }



        [HttpPost("register/applicant")]
        public async Task<IActionResult> RegisterApplicant([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return _responseHandler.BadRequest("Invalid data",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
            }

            var result = await _authService.RegisterApplicantAsync(userRegisterDTO);

            return result.Succeeded
                ? _responseHandler.Success(result.Message)
                : _responseHandler.BadRequest(result.Message,result.StatusCode, result.Errors);
        }
        [HttpPost("register/examiner")]

        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterExaminer([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return _responseHandler.BadRequest("Invalid data",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
            }

            var result = await _authService.RegisterApplicantAsync(userRegisterDTO);

            return result.Succeeded
                ? _responseHandler.Success(result.Message)
                : _responseHandler.BadRequest(result.Message,result.StatusCode, result.Errors);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return _responseHandler.BadRequest("Invalid data",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
            }

            var result = await _authService.LogInAsync(loginDTO);
            return result.Succeeded
                ? _responseHandler.Success(result.Message)
                : _responseHandler.BadRequest(result.Message, result.StatusCode, result.Errors);
        }

        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            if (email == null || token == null)
            {
                return _responseHandler.BadRequest("Invalid data",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
            }

            var result = await _authService.EmailConfirmationAsync(email, token);
            if (!result.Succeeded)
            {
                return _responseHandler.BadRequest(result.Message, result.Errors);
            }

            return Redirect("http://localhost:5173/login");
        }

        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return _responseHandler.BadRequest("Invalid data",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
            }

            var result = await _authService.ForgotPasswordAsync(dto.Email);

            return result.Succeeded
                ? _responseHandler.Success(result.Message)
                : _responseHandler.BadRequest(result.Message, result.StatusCode, result.Errors);

        }
        [HttpGet("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, [FromQuery] string token)
        {
            if(email == null) { return BadRequest(); }
            string encodedToken = Base64UrlEncoder.Encode(token);
            //HttpUtility.UrlEncode(token)
            return Redirect($"http://localhost:5173/resetpassword?email={email}&token={HttpUtility.UrlEncode(encodedToken)}");
        }

        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            if(dto == null) 
            { 
                return _responseHandler.BadRequest("Invalid input data", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
            }
            var result = await _authService.ResetPasswordAsync(dto);

            return result.Succeeded
               ? _responseHandler.Success(result.Message)
               : _responseHandler.BadRequest(result.Message, result.Errors);
           //Redirect("http://localhost:5173/login")
        }

        [HttpPost("changepassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            if (dto == null)
            {
                return _responseHandler.BadRequest("Invalid input data", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
            }
            var result = await _authService.ChangePasswordAsync(dto);
            return result.Succeeded
               ? _responseHandler.Success(result.Message)
               : _responseHandler.BadRequest(result.Message, result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _authService.DeleteUserAsync(id);
            return NoContent(); 
        }

        [HttpPut("updateuseremail")]
        public async Task<IActionResult> UpdateUserEmail(UpdateEmailDto dto)
        {
            if (dto == null)
            {
                return _responseHandler.BadRequest("Invalid input data", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
            }
            var result = await _authService.UpdateUserEmail(dto.Id, dto.newEmail);
            return result.Succeeded
              ? _responseHandler.Success(result.Message)
              : _responseHandler.BadRequest(result.Message, result.Errors); ;
        }




    }

} 

        
