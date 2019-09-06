using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CharacterGeneratorWeb.Models
{
    public class RegistrationModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email {get; set;}
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 20;
        public const string PasswordRequirementsMessage = "The Password must contain at least One Capital Letter, One Lowercase Letter, and One Special Character";
        public const string PasswordRequirements = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()+=?])).+$";
        public string Hash { get; set; }
        public string Salt { get; set; }
        [DataType("Password")]
        [Compare("PasswordAgain", ErrorMessage = "Passwords do not match")]
        [Required]
        [StringLength(MaxPasswordLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = MinPasswordLength)]
        [RegularExpression(PasswordRequirements, ErrorMessage = PasswordRequirementsMessage)]
        public string Password { get; set; }

        [DataType("Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Required]
        [StringLength(MaxPasswordLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = MinPasswordLength)]
        [RegularExpression(PasswordRequirements, ErrorMessage = PasswordRequirementsMessage)]
        [Display(Name = "Password Confirmation")]
        public string PasswordAgain { get; set; }
        public string Message { get; set; }
        
    }
}