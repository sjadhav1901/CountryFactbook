using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int ParentFeatureId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
