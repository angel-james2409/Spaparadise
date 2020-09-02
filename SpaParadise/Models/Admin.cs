using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SpaParadise.Models
{
    public class Admin
    {

        public int AdminId { get; set; }
        [Required]
        [ExcludeChar("!@.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }



        [DisplayName("Mobile Number")]
        [Required(ErrorMessage = "Please provide a Correct Phone Number")]
        [StringLength(10, MinimumLength = 10)]

        public String PhoneNo { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [MembershipPassword(
   MinRequiredNonAlphanumericCharacters = 1,
   MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
    ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Address { get; set; }
        [Required]
        [ExcludeChar("!@.")]
        public string Specialist { get; set; }
    }
}