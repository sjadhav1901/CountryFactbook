using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class User
    {
        public Guid AltId { get; set; }
        public Role RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
    }
}
