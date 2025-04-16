using Dev.AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;



namespace Dev.AuthService.Services
{
    /// <summary>
    /// Implementation of the authentication logic.
    /// </summary>
    public class AuthServiceClass:IAuthService
    {
        private readonly Dictionary<string, (string Hash, string Role)> _userStore = new(); // In-memory store
        private readonly PasswordHasher<string> _passwordHasher = new(); // Built-in secure hasher
        private readonly IConfiguration _config;
        private readonly ILogger<AuthServiceClass> _logger;

        public AuthServiceClass(IConfiguration config, ILogger<AuthServiceClass> logger)
        {
            _config = config;
            _logger = logger;
        }


        /// <summary>
        /// Register a user with email, username, password, and optional role.
        /// </summary>
        public async Task<AuthResult> RegisterAsync(RegisterRequest request)
        {
            if (_userStore.ContainsKey(request.Email.ToLower()))
            {
                return new AuthResult { IsSuccess = false, Message = "User already exists." };
            }

            // Hash password
            var hashedPassword = _passwordHasher.HashPassword(request.Email, request.Password);

            // Store user
            _userStore[request.Email.ToLower()] = (hashedPassword, request.Role);

            // Return JWT token
            var token = GenerateJwtToken(request.Email, request.Role, request.UserName);

            return new AuthResult
            {
                IsSuccess = true,
                Token = token,
                Role = request.Role,
                UserName = request.UserName,
                Message = "Registration successful."
            };
        }

        /// <summary>
        /// Login using email or username and validate password.
        /// </summary>
        public async Task<AuthResult> LoginAsync(LoginRequest request)
        {
            var email = request.UserNameOrEmail.ToLower();

            if (!_userStore.ContainsKey(email))
            {
                return new AuthResult { IsSuccess = false, Message = "User not found." };
            }

            var (storedHash, role) = _userStore[email];
            var result = _passwordHasher.VerifyHashedPassword(email, storedHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return new AuthResult { IsSuccess = false, Message = "Invalid credentials." };
            }

            var token = GenerateJwtToken(email, role, email.Split('@')[0]);

            return new AuthResult
            {
                IsSuccess = true,
                Token = token,
                Role = role,
                UserName = email,
                Message = "Login successful."
            };
        }

        /// <summary>
        /// Generates a JWT token based on user email and role.
        /// </summary>
        private string GenerateJwtToken(string email, string role, string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Hash a plain password using the default password hasher.
        /// </summary>
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null!, password);
        }

        /// <summary>
        /// Verify a plain password against the stored hash.
        /// </summary>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null!, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
