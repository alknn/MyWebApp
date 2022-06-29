using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebApp.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual Role Role { get; set; }
    }
}