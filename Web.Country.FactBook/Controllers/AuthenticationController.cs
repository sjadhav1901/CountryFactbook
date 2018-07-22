using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Db.Core.Utilites;
using Microsoft.AspNetCore.Http;
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
        [Route("api/authenticate/{rememberme}")]
        public User ValidateSignIn([FromBody] User model, int rememberme)
        {
            try
            {
                var passwordHash = _passwordHasher.HashPassword(model.Email, model.Password);
                var loggedUserByEmail = _userRepository.GetByEmail(model.Email);
                User user = AutoMapper.Mapper.Map<User>(loggedUserByEmail);
                if ((_passwordHasher.VerifyHashedPassword(model.Email, user.Password, model.Password) ==
                        PasswordVerificationResult.Success))
                {
                    if (rememberme==1)
                    {
                        user.Password = model.Password;
                        Set(user);
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
            }
            return new User { Id = 0 };
        }

        [HttpGet]
        [Route("api/cookies")]
        public User GetCookies()
        {
            try
            {
                var cookies = Request.Cookies["Credentials"];
                if (cookies != null)
                {
                    return AutoMapper.Mapper.Map<User>(Newtonsoft.Json.JsonConvert.DeserializeObject(cookies));
                }
            }
            catch(Exception)
            {
                Response.Cookies.Delete("Credentials");
            }
            return null;
        }

        public void Set(User value, int? expireTime = 10)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            var jsonCredentails = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            Response.Cookies.Append("Credentials", jsonCredentails, option);
        }

    }
}