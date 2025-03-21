using BCrypt.Net;
using AutoMapper;
using RepositoryLayer.Service;
using RepositoryLayer.Entity;
using ModelLayer.Model;
using BusinessLayer.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;


namespace BusinessLayer.Service
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _authRepo;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;
        private readonly IConfiguration _config;

        public AuthService(
            AuthRepository authRepo,
            JwtService jwtService,
            IMapper mapper,
            EmailService emailService,
            IConfiguration config)
        {
            _authRepo = authRepo;
            _jwtService = jwtService;
            _mapper = mapper;
            _emailService = emailService;
            _config = config;
        }

        // ✅ Register Method
        public ResponseModelSMD<UserResponse> Register(UserRequest dto)
        {
            var existingUser = _authRepo.GetUserByEmail(dto.Email);
            if (existingUser != null)
            {
                return new ResponseModelSMD<UserResponse>
                {
                    Success = false,
                    Message = "Email already in use"
                };
            }

            var userEntity = _mapper.Map<UserEntity>(dto);
            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var registeredUser = _authRepo.Register(userEntity);
            var userResponse = _mapper.Map<UserResponse>(registeredUser);

            return new ResponseModelSMD<UserResponse>
            {
                Success = true,
                Message = "Registration successful",
                Data = userResponse
            };
        }

        // ✅ Login Method
        public ResponseModelSMD<UserResponse> Login(LoginDTO dto)
        {
            var user = _authRepo.GetUserByEmail(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return new ResponseModelSMD<UserResponse>
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            var token = _jwtService.GenerateToken(user.Email);
            var userResponse = _mapper.Map<UserResponse>(user);
            userResponse.Token = token;

            return new ResponseModelSMD<UserResponse>
            {
                Success = true,
                Message = "Login successful",
                Data = userResponse
            };
        }
        public ResponseModelSMD<string> ForgotPassword(ForgotPasswordDTO model)
        {
            try
            {
                // ✅ Check if the email exists
                var user = _authRepo.GetUserByEmail(model.Email);
                if (user == null)
                {
                    return new ResponseModelSMD<string>
                    {
                        Success = false,
                        Message = "User with this email does not exist."
                    };
                }

                // ✅ Generate Reset Token with JWT Service
                string resetToken = _jwtService.GenerateResetToken(model.Email);

                // ✅ Send Reset Email
                SendResetEmail(model.Email, resetToken);

                return new ResponseModelSMD<string>
                {
                    Success = true,
                    Message = "Password reset email sent successfully",
                    Data = resetToken  // For debugging purposes (optional)
                };
            }
            catch (Exception ex)
            {
                return new ResponseModelSMD<string>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        // ✅ Send Reset Email Method
        private void SendResetEmail(string email, string resetToken)
        {
            try
            {
                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("bhardwajyash4u@gmail.com", "scpjmozfwhnpvoyy");
                    smtp.EnableSsl = true;

                    var mail = new MailMessage();
                    mail.From = new MailAddress("bhardwajyash4u@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "Password Reset";
                    mail.Body = $"Use this link to reset your password: https://localhost:7237/reset-password?token={resetToken}";

                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}");
            }
        }
        // ✅ Reset Password Method
        public ResponseModelSMD<string> ResetPassword(ResetPasswordDTO model)
        {
            try
            {
                // ✅ Validate Token and Extract Email
                var email = _jwtService.ValidateToken(model.Token);
                if (string.IsNullOrEmpty(email))
                {
                    return new ResponseModelSMD<string>
                    {
                        Success = false,
                        Message = "Invalid or expired token."
                    };
                }

                // ✅ Find User by Email
                var user = _authRepo.GetUserByEmail(email);
                if (user == null)
                {
                    return new ResponseModelSMD<string>
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }

                // ✅ Hash the New Password
                string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);

                // ✅ Update User Password
                user.Password = newHashedPassword;
                _authRepo.UpdateUser(user);

                return new ResponseModelSMD<string>
                {
                    Success = true,
                    Message = "Password reset successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResponseModelSMD<string>
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}

