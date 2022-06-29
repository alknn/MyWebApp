using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebApp.Models.ViewModel
{
    public class LoginViewModel
    {
        //[Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}