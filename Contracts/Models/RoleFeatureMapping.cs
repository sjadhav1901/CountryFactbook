using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class RoleFeatureMapping
    {
        public int Id { get; set; }
        public Role RoleId { get; set; }
        public int FeatureId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
