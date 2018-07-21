using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Db.Core.Utilites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Country.FactBook.Repositories;

namespace Web.Country.FactBook.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUserRepository _userRepository;
        private IPasswordHasher<string> _passwordHasher;
        public AuthenticationController(IUserRepository userRepository, IPasswordHasher<string> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [Route("api/authenticate")]
        public User ValidateSignIn([FromBody] User model)
        {
            try
            {
                var passwordHash = _passwordHasher.HashPassword(model.Email, model.Password);
                var loggedUserByEmail = _userRepository.GetByEmail(model.Email);
                User user = AutoMapper.Mapper.Map<User>(loggedUserByEmail);
                if ((_passwordHasher.VerifyHashedPassword(model.Email, user.Password, model.Password) ==
                        PasswordVerificationResult.Success))
                {
                    return user;
                }                    
            }
            catch (Exception ex)
            {
            }
            return new User { Id = 0 };
        }
    }
}