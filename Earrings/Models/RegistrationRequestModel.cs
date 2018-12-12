using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Earrings.Models
{
    public class RegistrationRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long!")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "No match. Please type in password again!")]
        public string PasswordConfirm { get; set; }
    }
}