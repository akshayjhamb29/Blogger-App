using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace microblog.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "UserEmailID")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }
        [Required]
        [Display(Name = "Contact")]
        public string Contact { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required]
        [Display(Name = "ProfileImage")]
        public string ProfileImage { get; set; }
    }
}