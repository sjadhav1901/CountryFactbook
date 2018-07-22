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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("api/authenticate/{rememberme}")]
        public User ValidateSignIn([FromBody] User model, int rememberme)
        {
            try
            {
                var loggedUserByEmail = _userRepository.GetByEmail(model.Email);
                User user = AutoMapper.Mapper.Map<User>(loggedUserByEmail);
                if ((_passwordHasher.VerifyHashedPassword(model.Email, user.Password, model.Password) ==
                        PasswordVerificationResult.Success))
                {
                    if (rememberme == 1)
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
            return null;
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
            catch (Exception)
            {
                Response.Cookies.Delete("Credentials");
            }
            return null;
        }

        public void Set(User value, int? expireTime = 24)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddHours(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddHours(2);
            var jsonCredentails = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            Response.Cookies.Append("Credentials", jsonCredentails, option);
        }

        [HttpGet]
        [Route("api/authenticate/forgot/{email}")]
        public User ForgotPassword(string email)
        {
            try
            {
                var user = _userRepository.GetByEmail(email);
                return AutoMapper.Mapper.Map<User>(user);
            }
            catch (Exception)
            {
            }
            return null;
        }

        [HttpGet]
        [Route("api/validate/reseruser/{altId}")]
        public User ValidateResetUserDetails(Guid altId)
        {
            try
            {
                Contracts.DataModels.User user = AutoMapper.Mapper.Map<Contracts.DataModels.User>(_userRepository.GetByAltId(altId));
                if(user !=null)
                {
                    return AutoMapper.Mapper.Map<User>(user);
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        [HttpPost]
        [Route("api/authenticate/resetpassword")]
        public User ResetPassword([FromBody] User model)
        {
            try
            {
                var passwordHash = _passwordHasher.HashPassword(model.Email, model.Password);
                Contracts.DataModels.User user = AutoMapper.Mapper.Map<Contracts.DataModels.User>(_userRepository.GetByAltId(model.AltId));
                if (user != null)
                {
                    user.Password = passwordHash;
                    user.UpdatedUtc = DateTime.UtcNow;
                    user.UpdatedBy = model.AltId;
                    var loggedUserByEmail = _userRepository.Save(user);
                    return AutoMapper.Mapper.Map<User>(user);
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}