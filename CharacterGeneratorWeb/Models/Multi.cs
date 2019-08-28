using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CharacterGeneratorWeb
{
    public class Multi
    {
        // User Stuff
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 20;
        public const string PasswordRequirementsMessage = "The Password must contain at least One Capital Letter, One Lowercase Letter, and One Special Character";
        public const string PasswordRequirements = @"^((?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()+=?])).+$";

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("PasswordAgain",ErrorMessage ="Passwords do not match")]
        [Required]
        [StringLength(MaxPasswordLength,ErrorMessage ="The {0} must be between {2} and {1} characters long.", MinimumLength =MinPasswordLength)]
        [RegularExpression(PasswordRequirements,ErrorMessage =PasswordRequirementsMessage)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage ="Passwords do not match")]
        [Required]
        [StringLength(MaxPasswordLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = MinPasswordLength)]
        [RegularExpression(PasswordRequirements, ErrorMessage = PasswordRequirementsMessage)]
        [Display(Name = "Password")]
        public string PasswordAgain { get; set; }
        public int RoleID { get; set; }

        // Role Stuff
        public string NewRoleName { get; set; }

        //Character Stuff
        [Required]
        public int UserID { get; set; }
        public string CharacterName { get; set; }
        public int ClassID { get; set; }
        public int RaceID { get; set; }
        public List<SelectListItem> ClassName { get; set; }
        public List<SelectListItem> RaceName { get; set; }

    }
}