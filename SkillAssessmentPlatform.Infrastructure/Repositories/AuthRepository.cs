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
                string activationLink = $"http://localhost:5112/api/auth/emailconfirmation?email={applicant.Email}&token={token}";

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
                                <h3>Activate Your Account</h3>
                                <p>Thank you for registering! Please click the button below to activate your account.</p>
                                <a href='{activationLink}' class='btn'>Activate Account</a>
                                <p>If the button doesn't work, you can also click on the following link:</p>
                                <p><a href='{activationLink}'>{activationLink}</a></p>
                                <p class='footer'>If you didn’t request this email, please ignore it.</p>
                            </div>
                        </body>
                        </html>";

                await _emailServices.SendEmailAsync(applicant.Email, "Account Activation", emailBody);
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
                string activationLink = $"http://localhost:5112/api/auth/emailconfirmation?email={examiner.Email}&token={token}";

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
                                <h3>Activate Your Account</h3>
                                <p>Thank you for registering! Please click the button below to activate your account.</p>
                                <a href='{activationLink}' class='btn'>Activate Account</a>
                                <p>If the button doesn't work, you can also click on the following link:</p>
                                <p><a href='{activationLink}'>{activationLink}</a></p>
                                <p class='footer'>If you didn’t request this email, please ignore it.</p>
                            </div>
                        </body>
                        </html>";

                await _emailServices.SendEmailAsync(examiner.Email, "Account Activation", emailBody);
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

        // UNUSED
        private async Task<string> SendEmailConfirmation( User user)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string activationLink = $"http://localhost:5112/api/auth/emailconfirmation?email={user.Email}&token={token}";

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
                            <h3>Activate Your Account</h3>
                            <p>Thank you for registering! Please click the button below to activate your account.</p>
                            <a href='{activationLink}' class='btn'>Activate Account</a>
                            <p>If the button doesn't work, you can also click on the following link:</p>
                            <p><a href='{activationLink}'>{activationLink}</a></p>
                            <p class='footer'>If you didn’t request this email, please ignore it.</p>
                        </div>
                    </body>
                    </html>";

                await _emailServices.SendEmailAsync(user.Email, "Account Activation", emailBody);
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
                string resetLink = $"http://localhost:5112/api/auth/resetpassword?email={user.Email}&token={token}";

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
                                <h3>Reset password</h3>
                                <p>You can click the botton to reset your password.</p>
                                <a href='{resetLink}' class='btn'>RESET PASSWORD</a>
                                <p>If the button doesn't work, you can also click on the following link:</p>
                                <p><a href='{resetLink}'>{resetLink}</a></p>
                                <p class='footer'>If you didn’t request this email, please ignore it.</p>
                            </div>
                        </body>
                        </html>";

                await _emailServices.SendEmailAsync(user.Email, "Forgot Password", emailBody);
                return "1";
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
