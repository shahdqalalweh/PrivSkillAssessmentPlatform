using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Core.Common;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Exceptions;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Infrastructure.ExternalServices;
using SkillAssessmentPlatform.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SkillAssessmentPlatform.Application.Services
{
    public class AuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly TokenService _tokenService;
        private readonly EmailServices _emailService;

        public AuthService(IAuthRepository authRepository, 
            IMapper mapper,
            ILogger<AuthService> logger,
            TokenService tokenService,
            EmailServices emailService)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _logger = logger;
            _tokenService = tokenService;
            _emailService = emailService;
        }
        /*
        public async Task<string> RegisterApplicantAsync(UserRegisterDTO dto)
        {
            if(dto == null)
            {
                return "0";
            }
            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                return "0";
            }

            var user = _mapper.Map<User>(dto);

            return await _authRepository.RegisterApplicantAsync(user, dto.Password);
        }
        */
            public async Task<Response<Applicant>> RegisterApplicantAsync(UserRegisterDTO dto)
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Password))
                {
                    return new Response<Applicant>(null, "Invalid input data", HttpStatusCode.BadRequest);
                }

                try
                {
                    var user = _mapper.Map<User>(dto);
                    var applicant = await _authRepository.RegisterApplicantAsync(user, dto.Password);

                    return new Response<Applicant>(applicant,"User registered & email sent", HttpStatusCode.OK);
                }
                catch (UserException ex)
                {
                    return new Response<Applicant>( "User creation failed", HttpStatusCode.BadRequest, new List<string> { ex.Message });
                }
                catch (Exception)
                {
                    return new Response<Applicant>("User registered but email sending failed", HttpStatusCode.InternalServerError);
                }
            }

        public async Task<Response<Examiner>> RegisterExaminerAsync(UserRegisterDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Password))
            {
                return new Response<Examiner>(null, "Invalid input data", HttpStatusCode.BadRequest);
            }

            try
            {
                var user = _mapper.Map<User>(dto);
                var examiner = await _authRepository.RegisterExaminerAsync(user, dto.Password);

                return new Response<Examiner>(examiner, "User registered & email sent", HttpStatusCode.OK);
            }
            catch (UserException ex)
            {
                return new Response<Examiner>("User creation failed", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (Exception)
            {
                return new Response<Examiner>("User registered but email sending failed", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<Response<string>> EmailConfirmationAsync(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return new Response<string>(null, "Email or token is missing.", HttpStatusCode.BadRequest);
            }

            try
            {
                await _authRepository.EmailConfirmation(email, token);
                return new Response<string>("Confirmation succeeded", "Email successfully confirmed.", HttpStatusCode.OK);
            }
            catch (UserException ex)
            {
                return new Response<string>("Email confirmation error" ,HttpStatusCode.BadRequest, new List<string> { ex.Message });

            }
            catch (UserNotFoundException ex)
            {
                return new Response<string>("Email is no valid", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($" Unexpected error during email confirmation: {ex.Message}");
                return new Response<string>(null, "Email confirmation failed due to an unexpected error.", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<string>> LogInAsync(LoginDTO loginDTO)
        {
            try
            {
                var user = await _authRepository.LogInAsync(loginDTO.Email, loginDTO.Password);
                var token = _tokenService.GenerateToken(user);
                return new Response<string>(token, HttpStatusCode.OK);

            } catch (UserException ex)
            {
                return new Response<string>("Not correct Password", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (UserNotFoundException ex)
            {
                return new Response<string>("Not correct Email", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (Exception)
            {
                return new Response<string>("Erorr in genrate token", HttpStatusCode.InternalServerError);
            }

        }
        public async Task<Response<string>> ForgotPasswordAsync(string email)
        {
            try
            {
                await _authRepository.ForgotPasswordAsync(email);
                return new Response<string>("Email sent", HttpStatusCode.OK);
            }
            catch (UserNotFoundException ex)
            {
                return new Response<string>("Email is not exist", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (Exception)
            {
                return new Response<string>("User registered but email sending failed", HttpStatusCode.InternalServerError);
            }

        }
        public async Task<Response<string>> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            try
            {
                await _authRepository.ResetPassword(dto.Email, dto.Password, dto.Token);
                return new Response<string>("Password reset successful", HttpStatusCode.OK);
            }
            catch (UserException ex)
            {
                return new Response<string>("Error resetting password", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (UserNotFoundException ex)
            {
                return new Response<string>("Email is not exist", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (Exception ex)
            {
                return new Response<string>( "Email reseting failed due to an unexpected error.", HttpStatusCode.InternalServerError, new List<string> { ex.Message });
            }

        }
        public async Task<Response<string>> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            try
            {
                await _authRepository.ChangePasswordAsync(dto.Email, dto.OldPassword, dto.NewPassword);
                return new Response<string>("Password change successfully", HttpStatusCode.OK);
            }
            catch (UserNotFoundException ex)
            {
                return new Response<string>("Email is not exist", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (UserException ex)
            {
                return new Response<string>("Cahnging Password field", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            
        }
        public async Task DeleteUserAsync(string id)
        {
            await _authRepository.DeleteUserAsync(id);
        }

        public async Task<Response<string>> UpdateUserEmail(string userId, string newEmail)
        {
            try
            {
                await _authRepository.UpdateUserEmail(userId, newEmail);
                return new Response<string>("Email updated", HttpStatusCode.OK);
            }
            catch (UserException ex)
            {
                return new Response<string>("Field", HttpStatusCode.BadRequest, new List<string> { ex.Message });
            }
            catch (Exception)
            {
                return new Response<string>("Field", HttpStatusCode.InternalServerError);
            }
        }
    }
}
