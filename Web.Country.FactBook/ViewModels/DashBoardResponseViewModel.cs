using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.ViewModels
{
    public class DashBoardResponseViewModel
    {
        public List<Contracts.Models.RecentActivity> RecentActivity { get; set; }
        public Contracts.Models.User User { get; set; }
    }
}
