using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.AuthService.Models
{
    /// <summary>
    /// Represents the login input model.
    /// </summary>

    public class LoginRequest
    {
        /// <summary>Can be username or email.</summary>
        public string UserNameOrEmail { get; set; }

        /// <summary>Login password (to be verified).</summary>
        public string Password { get; set; }
    }
}
