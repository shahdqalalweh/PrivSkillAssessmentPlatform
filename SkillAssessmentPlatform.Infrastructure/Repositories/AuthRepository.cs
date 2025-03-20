using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SkillAssessmentPlatform.Core.Common;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Enums;
using SkillAssessmentPlatform.Core.Exceptions;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Infrastructure.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SkillAssessmentPlatform.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly EmailServices _emailServices;
        private readonly ILogger<AuthRepository> _logger;


        public AuthRepository(UserManager<User> userManager,
            IConfiguration configuration,
            EmailServices emailServices,
            ILogger<AuthRepository> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailServices = emailServices;
            _logger = logger;
        }


        public async Task<Applicant> RegisterApplicantAsync(User user, string password)
        {
            var applicant = new Applicant
            {
                Email = user.Email,
                UserName = user.Email,
                UserType = Actors.Applicant,
                FullName = user.FullName,
                Status = ApplicantStatus.Inactive,
            };

            var result = await _userManager.CreateAsync(applicant, password);
            if (!result.Succeeded)
            {
                throw new UserException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicant);
            string message = "Thank you for registering! Please click the button below to activate your account.";

            await SendEmailAsync(applicant.Email, token, "emailconfirmation", "Account Activation", "Activate Your Account", message);
            var newApplicant = new Applicant
            {
                Id = applicant.Id,
                Email = user.Email,
                UserName = user.Email,
                UserType = Actors.Applicant,
                FullName = user.FullName,
                Status = ApplicantStatus.Inactive,
            };
            //if (sendErs)
            await _userManager.AddToRoleAsync(applicant, Actors.Applicant.ToString());
            return newApplicant;
        }

        public async Task<Examiner> RegisterExaminerAsync(User user, string password)
        {
            var examiner = new Examiner
            {
                Email = user.Email,
                UserName = user.Email,
                UserType = Actors.Examiner,
                FullName = user.FullName,
                Specialization = "----",

            };
            var result = await _userManager.CreateAsync(examiner, password);

            if (!result.Succeeded)
            {
                //var errorMasseges = result.Errors.Select(e => e.Description).ToList();
                //return string.Join(", ", errorMasseges);
                throw new UserException(string.Join(", ", result.Errors.Select(e => e.Description)));

            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(examiner);
            string message = "Thank you for registering! Please click the button below to activate your account.";

            await SendEmailAsync(examiner.Email, token, "emailconfirmation", "Account Activation", "Activate Your Account", message);

            var newExaminer = new Examiner
            {
                Id = examiner.Id,
                Email = user.Email,
                UserName = user.Email,
                UserType = Actors.Examiner,
                FullName = user.FullName,

            };
            //if (sendErs)
            await _userManager.AddToRoleAsync(examiner, Actors.Examiner.ToString());
            return newExaminer;

        }

        public async Task EmailConfirmation(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException("Invalid email.");
            }
            string decodedToken = Uri.UnescapeDataString(token);

            var confirmResult = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!confirmResult.Succeeded)
            {
                foreach (var error in confirmResult.Errors)
                {
                    _logger.LogError($" Email confirmation error: {error.Code} - {error.Description}");
                }
                throw new UserException(string.Join(", ", confirmResult.Errors.Select(e => e.Description)));
            }

        }

        public async Task<bool> SendEmailAsync(string email, string token, string endpoint, string subject, string action, string message)
        {
            try
            {
                _logger.LogWarning("\n\n SendEmailAsync method =====>  " + token);

                // var decodedToken = Uri.UnescapeDataString(token);
                //string encodedToken = HttpUtility.UrlEncode(token);
                string encodedToken = Base64UrlEncoder.Encode(token);

                _logger.LogWarning("\n\n ==== AFTER encoding  =====>  " + encodedToken);

                string link = $"http://localhost:5112/api/auth/{endpoint}?email={email}&token={encodedToken}";

                _logger.LogInformation($"\n\n[Email Sending] Email: {email}, Endpoint: {endpoint}, token : {token}--- Encoded Token: {encodedToken}");

                string emailBody = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }}
                            .container {{
                                width: 80%;
                                max-width: 600px;
                                margin: 20px auto;
                                background: #ffffff;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                text-align: center;
                            }}
                            h3 {{
                                color: #333;
                            }}
                            p {{
                                color: #555;
                                font-size: 16px;
                            }}
                            .btn {{
                                display: inline-block;
                                padding: 12px 20px;
                                margin-top: 10px;
                                font-size: 16px;
                                color: #fff;
                                background-color: #28a745;
                                text-decoration: none;
                                border-radius: 5px;
                            }}
                            .btn:hover {{
                                background-color: #218838;
                            }}
                            .footer {{
                                margin-top: 20px;
                                font-size: 12px;
                                color: #999;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h3>{action}</h3>
                            <p>{message}</p>
                            <a href='{link}' class='btn'>{action}</a>
                            <p>If the button doesn't work, you can also click on the following link:</p>
                            <p><a href='{link}'>{link}</a></p>
                            <p class='footer'>If you didn’t request this email, please ignore it.</p>
                        </div>
                    </body>
                    </html>";

                await _emailServices.SendEmailAsync(email, subject, emailBody);
                _logger.LogInformation($"[Email Sent] Successfully sent email to {email}.");

                return true;
            }

            catch (Exception ex)
            {
                _logger.LogError($"[Unexpected Error] While sending email to {email}: {ex.Message}");
                throw;
            }
        }


        public async Task ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException("Invalid email.");
            }

            //// check old password
            //var oldPasswordValid = await _userManager.CheckPasswordAsync(user, oldPassword);
            //if (!oldPasswordValid)
            //{
            //    return "Old password is incorrect.";
            //}

            // change password
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    _logger.LogError($"Error Code: {error.Code}, Description: {error.Description}");
                }
                throw new UserException(string.Join(", ", changePasswordResult.Errors.Select(e => e.Description)));
            }

        }

        public async Task<User>? LogInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException("Invalid email.");
            }

            if (!(await _userManager.CheckPasswordAsync(user, password)))
            {
                throw new UserException("Invalid password.");
            }
            return user;
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException("Invalid email.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            _logger.LogWarning("\n\n ====>\n token after genrate =    " + token);
            string message = "\r\nThank you for reaching out to us. We have received your request to reset your password.\r\n\r\nTo proceed with resetting your password, please follow the instructions below:\r\n\r\nClick on the password reset link sent to your registered email address.\r\nFollow the prompts to create a new password.\r\n";
            await SendEmailAsync(user.Email, token, "resetpassword", "Forgot Password", "Reset your password", message);

        }
        public async Task ResetPassword(string email, string password, string token)
        {
            //var newtoken = HttpUtility.UrlDecode(token);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UserNotFoundException("Invalid email.");
            }
            _logger.LogInformation("\n\n ======> Recived token = "+token);
            // var decodedToken = WebUtility.UrlDecode(token);
            //var decodedToken = Uri.UnescapeDataString(token);
            string decodedToken = Base64UrlEncoder.Decode(token);
            //var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));


            _logger.LogInformation("\n\n ======> Decoded token = " + decodedToken);
            var resetPassResult = await _userManager.ResetPasswordAsync(user, decodedToken, password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                        _logger.LogError(error.Code, error.Description);
                    throw new UserException(string.Join(", ", resetPassResult.Errors.Select(e => e.Description)));
                }
            }
        //string decodedToken = Uri.UnescapeDataString(token);
        //string decodedToken = Uri.UnescapeDataString(token);

        public async Task DeleteUserAsync(string id)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }

            public async Task<bool> UpdateUserEmail(string userId, string newEmail)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    throw new UserNotFoundException("Invalid email.");
                var existUser = await _userManager.FindByEmailAsync(newEmail);
                if (existUser != null)
                    throw new UserException("New Email already exist");

                var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
                var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
                if (!result.Succeeded)
                {
                    throw new UserException(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                user.UserName = newEmail;
                var updateUserNameResult = await _userManager.UpdateAsync(user);
                if (!updateUserNameResult.Succeeded)
                {
                    throw new UserException(string.Join(", ", updateUserNameResult.Errors.Select(e => e.Description)));
                }

                return updateUserNameResult.Succeeded;
            }
        }
    }
