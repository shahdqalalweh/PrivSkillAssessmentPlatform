using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Enums;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Infrastructure.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        
        public async Task<string> RegisterApplicantAsync(User user, string password)
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
                var errorMasseges = result.Errors.Select(e => e.Description).ToList();
                return string.Join(", ", errorMasseges);
            }
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicant);
                string message = "Thank you for registering! Please click the button below to activate your account.";
                await SendEmailConfirmation(applicant.Email, token, "emailconfirmation", "Account Activation", "Activate Your Account", message);

            }catch (Exception ex)
            {
                return ex.Message;
            }
            return "Confirmation Email sent";

             //return await SendEmailConfirmation(user);
        }

        public async Task<string> RegisterExaminerAsync(User user, string password)
        {
            var examiner = new Examiner
            {
                Email = user.Email,
                UserName = user.Email,
                UserType = Actors.Examiner,
                FullName = user.FullName,
                Specialization = "----",
                MaxWorkLoad = 9,
                CurrWorkLoad = 0
            };
            var result = await _userManager.CreateAsync(examiner, password);

            if (!result.Succeeded)
            {
                var errorMasseges = result.Errors.Select(e => e.Description).ToList();
                return string.Join(", ", errorMasseges);
            }
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(examiner);
                string message = "Thank you for registering! Please click the button below to activate your account.";
                await SendEmailConfirmation(examiner.Email, token, "emailconfirmation" , "Account Activation", "Activate Your Account", message);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Confirmation Email sent";

            //return await SendEmailConfirmation(user);

        }
        public async Task<string> EmailConfirmation(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "In correct email";
            }
            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded) 
            {
                foreach (var error in confirmResult.Errors)
                {
                    _logger.LogError($"❌ Email confirmation error: {error.Code} - {error.Description}");
                }
                return "confirmation unsucceeeded";
            }
            return "1";
        }
        private async Task<string> SendEmailConfirmation( string email, string token, string endpoint, string subject, string action, string message)
        {
            try
            {
                string Link = $"http://localhost:5112/api/auth/{endpoint}?email={email}&token={token}";

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
                            <a href='{Link}' class='btn'>{action}</a>
                            <p>If the button doesn't work, you can also click on the following link:</p>
                            <p><a href='{Link}'>{Link}</a></p>
                            <p class='footer'>If you didn’t request this email, please ignore it.</p>
                        </div>
                    </body>
                    </html>";

                await _emailServices.SendEmailAsync(email, subject, emailBody);
            }catch (Exception ex)
            {
                return ex.Message;
            }
            return "1";
            
        }
        public async Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "User not found.";
            }

            // check old password
            var oldPasswordValid = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!oldPasswordValid)
            {
                return "Old password is incorrect.";
            }

            // change password
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    _logger.LogError($"Error Code: {error.Code}, Description: {error.Description}");
                }
                return "Error in changing password.";
            }

            return "1";
        }

        public async Task<User>? LogInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user == null)
            {
                return null;
            }
            if (!(await _userManager.CheckPasswordAsync(user, password)))
            {
                return null;
            }
            return user;

        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if( user != null )
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                string message = "\r\nThank you for reaching out to us. We have received your request to reset your password.\r\n\r\nTo proceed with resetting your password, please follow the instructions below:\r\n\r\nClick on the password reset link sent to your registered email address.\r\nFollow the prompts to create a new password.\r\n";
                return await SendEmailConfirmation(user.Email, token, "resetpassword", "Forgot Password", "Reset your password", message);

            }
            return "couldnot send an email";
        }
        public async Task<string> ResetPassword(string email, string password, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return "faild";
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    _logger.LogError(error.Code, error.Description);
                return "error in reseting procces";
            }
            return "1";
        }


    }
}
