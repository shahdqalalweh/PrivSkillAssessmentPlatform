using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using SkillAssessmentPlatform.Application.DTOs.Auth;
using SkillAssessmentPlatform.Core.Common;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Exceptions;
using SkillAssessmentPlatform.Core.Interfaces.Repository;
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

        public async Task<string> RegisterApplicantAsync(UserRegisterDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new ArgumentException("Invalid input data");
            }

            var user = _mapper.Map<User>(dto);
            return await _authRepository.RegisterApplicantAsync(user, dto.Password);
        }

        public async Task<string> RegisterExaminerAsync(UserRegisterDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new BadRequestException("Invalid input data");
            }

            var user = _mapper.Map<User>(dto);
            return await _authRepository.RegisterExaminerAsync(user, dto.Password);
        }
        public async Task EmailConfirmationAsync(string email, string token)
        {

            await _authRepository.EmailConfirmation(email, token);
        }
        /*
         * old login
        public async Task<Response<string>> LogInAsync(LoginDTO loginDTO)
        {
            try
            {
                var user = await _authRepository.LogInAsync(loginDTO.Email, loginDTO.Password);
                var token = _tokenService.GenerateToken(user);
                return new Response<string>(token, HttpStatusCode.OK);

            } catch (BadRequestException ex)
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
        */
        public async Task<string> LogInAsync(LoginDTO loginDTO)
        {
            var user = await _authRepository.LogInAsync(loginDTO.Email, loginDTO.Password);
            return _tokenService.GenerateToken(user);
        }

        public async Task ForgotPasswordAsync(string email)
        {
            await _authRepository.ForgotPasswordAsync(email);
        }
        public async Task ResetPasswordAsync(ResetPasswordDTO dto)
        {
            await _authRepository.ResetPassword(dto.Email, dto.Password, dto.Token);
        }
        public async Task ChangePasswordAsync(ChangePasswordDTO dto)
        {
            await _authRepository.ChangePasswordAsync(dto.Email, dto.OldPassword, dto.NewPassword);
        }

        public async Task UpdateUserEmailAsync(string userId, string newEmail)
        {
            await _authRepository.UpdateUserEmail(userId, newEmail);
        }
    }
}
