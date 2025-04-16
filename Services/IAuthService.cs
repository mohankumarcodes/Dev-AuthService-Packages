using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dev.AuthService.Models;

namespace Dev.AuthService.Services
{
    /// <summary>
    /// Interface that defines methods for user authentication.
    /// </summary>
    
    public interface IAuthService
    {
        /// <summary>
        /// Register a new user with credentials and return auth result.
        /// </summary>
        Task<AuthResult> RegisterAsync(RegisterRequest request);

        /// <summary>
        /// Log in a user using email or username.
        /// </summary>
        Task<AuthResult> LoginAsync(LoginRequest request);

        /// <summary>
        /// Hashing Password.
        /// </summary>
        string HashPassword(string password);

        /// <summary>
        /// Verfy password.
        /// </summary>
        bool VerifyPassword(string password, string hashedPassword);
    }
}
