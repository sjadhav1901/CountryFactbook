using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Db.Core.Utilites;
using Microsoft.AspNetCore.Mvc;
using Web.Country.FactBook.Repositories;

namespace Web.Country.FactBook.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUserRepository _userRepository { get; }
        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        [Route("api/authenticate")]
        public Contracts.DataModels.User ValidateSignIn()
        {
            Contracts.DataModels.User user = _userRepository.Get(new Contracts.DataModels.User
            {
                Id = 1
            });
            return user;
        }
    }
}