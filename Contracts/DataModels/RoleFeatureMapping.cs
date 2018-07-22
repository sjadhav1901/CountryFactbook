using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DataModels
{
    public class RoleFeatureMapping : IId<int>
    {
        public int Id { get; set; }
        public Role RoleId { get; set; }
        public int FeatureId { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
