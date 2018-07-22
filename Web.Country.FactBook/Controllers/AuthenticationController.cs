using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Db.Core.Utilites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Country.FactBook.Helpers;
using Web.Country.FactBook.Repositories;

namespace Web.Country.FactBook.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUserRepository _userRepository;
        private IPasswordHasher<string> _passwordHasher;
        private IActivityHelper _activityHelper;
        public AuthenticationController(IUserRepository userRepository, IPasswordHasher<string> passwordHasher, IActivityHelper activityHelper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _activityHelper = activityHelper;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult Register()
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
                    _activityHelper.SaveActivity("Sign In", "You have signed into your account on " + DateTime.UtcNow.ToString() +" (GMT)", user.AltId);
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
                if (user != null)
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
                    _activityHelper.SaveActivity("Reset Password", "You have successfully reseted your password on " + DateTime.UtcNow.ToString() + " (GMT)", user.AltId);
                    return AutoMapper.Mapper.Map<User>(user);
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        [HttpPost]
        [Route("api/register")]
        public User Register([FromBody] User model)
        {
            try
            {
                var user = _userRepository.GetByEmail(model.Email);
                if (user == null)
                {
                    var passwordHash = _passwordHasher.HashPassword(model.Email, model.Password);
                    Guid guid = Guid.NewGuid();
                    user = _userRepository.Save(new Contracts.DataModels.User
                    {
                        AltId = guid,
                        RoleId = Contracts.Enums.Role.User,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = passwordHash,
                        IsEnabled = true,
                        CreatedUtc = DateTime.UtcNow,
                        CreatedBy = guid
                    });
                }
                _activityHelper.SaveActivity("Sign up", "You have completed registartion with an email id " + model.Email + " on " + DateTime.UtcNow.ToString() + " (GMT)", user.AltId);
                return AutoMapper.Mapper.Map<User>(user);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}