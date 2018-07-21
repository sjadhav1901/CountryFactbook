using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Country.FactBook.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        [Route("api/authenticate")]
        public IEnumerable<User> ValidateSignIn()
        {
            List<User> user = new List<User>
           {
               new User{
                   Id=1,
                   FirstName="Sachin",
                   LastName="Jadhav",
                   Email="sjadhav1901@gmasil.com"
               }
           };
            return user;
        }
    }
}