using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Country.FactBook.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}