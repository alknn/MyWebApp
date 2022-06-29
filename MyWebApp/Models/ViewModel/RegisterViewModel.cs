using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWebApp.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(6)]
        public string PasswordConfirmed { get; set; }
        public string RoleName { get; set; }
    }
}