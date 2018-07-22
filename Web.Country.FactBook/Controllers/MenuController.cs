using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Controllers
{
    public class MenuController : Controller
    {
        [HttpGet]
        [Route("api/authednavmenu/{altId}")]
        public ActionResult AuthedNavMenu(Guid altId)
        {
            return PartialView("PartialViews/_AuthedNavMenu");
        }
    }
}
