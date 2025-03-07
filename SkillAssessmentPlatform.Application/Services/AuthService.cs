using AutoMapper;
using Microsoft.Extensions.Logging;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IMapper mapper
           , ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<string> RegisterApplicant(UserRegisterDTO dto, string password)
        {
            _logger.LogInformation("AuthService-RegisterApplicant");
            // Console.WriteLine(dto);
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Invailed data";
            }
            if (dto == null)
            {
                return "Invailed data";
            }
            var user = _mapper.Map<User>(dto);
            _logger.LogInformation("Mapping done");
            if (user == null)
            {
                return "Mapping failed";
            }
            Console.WriteLine(user);
            return await _userRepository.RegisterApplicantAsync(user, password);
        }
        public async Task<string> RegisterExaminer(UserRegisterDTO dto, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Invailed data";
            }
            if (dto == null)
            {
                return "Invailed data";
            }
            var user = _mapper.Map<User>(dto);
            return await _userRepository.RegisterExaminerAsync(user, password);
        }

    }
}
