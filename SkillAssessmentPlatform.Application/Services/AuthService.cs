using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Infrastructure.ExternalServices;
using SkillAssessmentPlatform.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<string> RegisterExaminerAsync(UserRegisterDTO dto)
        {
            if (dto == null)
            {
                return "0";
            }
            if (string.IsNullOrWhiteSpace(dto.Password))
            {
                return "0";
            }

            var user = _mapper.Map<User>(dto);
           
            
            return await _authRepository.RegisterExaminerAsync(user, dto.Password);
        }
        public Task<string> EmailConfirmation(string email, string token)
        {
            return _authRepository.EmailConfirmation(email, token);
        }
        public async Task<string> LogInAsync(LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return "0";
            }
            var user = await _authRepository.LogInAsync(loginDTO.Email, loginDTO.Password);
            if(user == null)
            {
                return "0";
            }
            return _tokenService.GenerateToken(user);

        }
        public async Task<string> ForgotPasswordAsync(string email)
        {
            return await _authRepository.ForgotPasswordAsync(email);
        }
        public async Task<string> ResetPasswordAsync(ResetPasswordDTO dto)
        {
            return await _authRepository.ResetPassword(dto.Email, dto.Password, dto.token);
        }
        public async Task<string> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            return await _authRepository.ChangePasswordAsync(dto.Email, dto.OldPassword, dto.NewPassword);
        }




    }
}
