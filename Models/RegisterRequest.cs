using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.AuthService.Models
{
    /// <summary>
    /// Represents the data needed for a new user registration.
    /// </summary>
   
    public class RegisterRequest
    {
        /// <summary>User's display name or username.</summary>
        public string UserName { get; set; }


        /// <summary>Email used for login and notifications.</summary>
        public string Email { get; set; }


        /// <summary>User's password (will be hashed before storing).</summary>
        public string Password { get; set; }


        /// <summary>Optional: Role of the user (e.g., Admin, User).</summary>
        public string Role { get; set; } = "User"; // Default role
    }
}
