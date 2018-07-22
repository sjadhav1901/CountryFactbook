using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class RecentActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedUtc { get; set; }
    }
}
