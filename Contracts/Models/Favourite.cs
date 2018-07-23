using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public Guid CountryAltId { get; set; }
        public bool IsEnabled { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
