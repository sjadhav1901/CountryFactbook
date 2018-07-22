using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.ViewModels
{
    public class MenueResponseViewModel
    {
        public List<Contracts.Models.Feature> Features { get; set; }
        public Contracts.Models.User User { get; set; }
    }
}
