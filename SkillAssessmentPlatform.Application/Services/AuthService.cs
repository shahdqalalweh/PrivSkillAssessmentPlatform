using AutoMapper;
using Microsoft.Extensions.Logging;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Interfaces;
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

        public AuthService(IAuthRepository authRepository, 
            IMapper mapper,
            ILogger<AuthService> logger,
            TokenService tokenService)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _logger = logger;
            _tokenService = tokenService;
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
        

        

    }
}
