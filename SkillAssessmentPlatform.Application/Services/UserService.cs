using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SkillAssessmentPlatform.Application.DTOs.Auth;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillAssessmentPlatform.Infrastructure.Repositories;
using SkillAssessmentPlatform.Core.Common;
using SkillAssessmentPlatform.Infrastructure.ExternalServices;
using SkillAssessmentPlatform.Core.Interfaces.Repository;

namespace SkillAssessmentPlatform.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IFileService fileService,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _fileService = fileService;
            _logger = logger;
        }

        public async Task<UserDTO> GetProfileAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateProfileAsync(string userId, UpdateUserDTO updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            // Update user properties
            user.FullName = updateUserDto.FullName;
            user.DateOfBirth = updateUserDto.DateOfBirth;
            user.Gender = updateUserDto.Gender;

            // Save changes
            var updatedUser = await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserDTO>(updatedUser);
        }



        public async Task<string> UpdateProfileImageAsync(string userId, IFormFile image)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (image == null || image.Length == 0)
                throw new BadRequestException("No image provided");

            // Delete old image if exists
            if (!string.IsNullOrEmpty(user.Image) && user.Image != "default.png")
            {
                await _fileService.DeleteFileAsync(user.Image);
            }

            // Upload new image
            var imagePath = await _fileService.UploadFileAsync(image, "profile-images");
            user.Image = imagePath;

            await _userRepository.UpdateAsync(user);

            return imagePath;
        }

        public async Task<PagedResponse<UserDTO>> GetAllUsersAsync(string userType = null, int page = 1, int pageSize = 10)
        {
            IEnumerable<User> users;
            int totalCount;

            if (!string.IsNullOrEmpty(userType) && Enum.TryParse<Actors>(userType, out var userTypeEnum))
            {
                users = await _userRepository.GetUsersByTypeAsync(userTypeEnum, page, pageSize);
                totalCount = await _userRepository.GetCountByTypeAsync(userTypeEnum);
            }
            else
            {
                users = await _userRepository.GetPagedAsync(page, pageSize);
                totalCount = await _userRepository.GetTotalCountAsync();
            }
            var userDtos = _mapper.Map<IEnumerable<UserDTO>>(users);
            return new PagedResponse<UserDTO>(
                userDtos,
                page,
                pageSize,
                totalCount
            );

        }
    

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }
/*
        public async Task<PagedResponse<UserDTO>> SearchUsersAsync(string searchTerm, string userType = null, int page = 1, int pageSize = 10)
        {
            IEnumerable<User> users;
            int totalCount;

            if (!string.IsNullOrEmpty(userType) && Enum.TryParse<Actors>(userType, out var userTypeEnum))
            {
                users = await _userRepository.SearchUsersByTypeAsync(searchTerm, userTypeEnum, page, pageSize);
                totalCount = await _userRepository.CountSearchByTypeAsync(searchTerm, userTypeEnum);
            }
            else
            {
                users = await _userRepository.SearchUsersAsync(searchTerm, page, pageSize);
                totalCount = await _userRepository.CountSearchAsync(searchTerm);
            }

            return new PagedResponse<UserDTO>(
                _mapper.Map<List<UserDTO>>(users),
                page,
                pageSize,
                totalCount
            );
        }
        */

        public async Task<bool> DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
            return true;
        }
    }
}
