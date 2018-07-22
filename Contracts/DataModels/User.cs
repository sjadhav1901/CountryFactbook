using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DataModels
{
    public class User
    {
        public long Id { get; set; }
        public Role RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
