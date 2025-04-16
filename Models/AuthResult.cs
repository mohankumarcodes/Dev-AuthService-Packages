using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.AuthService.Models
{
    /// <summary>
    /// Represents the result returned after successful authentication.
    /// </summary>
    
    public class AuthResult
    {
        /// <summary>Authentication status.</summary>
        public bool IsSuccess { get; set; }

        /// <summary>JWT token if login/register succeeds.</summary>
        public string Token { get; set; }

        /// <summary>Optional message for success or error.</summary>
        public string Message { get; set; }

        /// <summary>User's assigned role.</summary>
        public string Role { get; set; }

        /// <summary>Authenticated username.</summary>
        public string UserName { get; set; }
    }
}
