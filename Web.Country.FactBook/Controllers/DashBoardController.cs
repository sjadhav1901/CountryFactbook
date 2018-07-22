using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Country.FactBook.Repositories;
using Web.Country.FactBook.ViewModels;

namespace Web.Country.FactBook.Controllers
{
    public class DashBoardController : Controller
    {
        private IRecentActivityRepository _recentActivityRepository;
        private IUserRepository _userRepository;
        public DashBoardController(IRecentActivityRepository recentActivityRepository, IUserRepository userRepository)
        {
            _recentActivityRepository = recentActivityRepository;
            _userRepository = userRepository;
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        [Route("api/dashboard/{altId}")]
        public DashBoardResponseViewModel DashBoard(Guid altId)
        {
            try
            {
                Contracts.DataModels.User user = AutoMapper.Mapper.Map<Contracts.DataModels.User>(_userRepository.GetByAltId(altId));
                IEnumerable<Contracts.DataModels.RecentActivity> RecentActivity = _recentActivityRepository.GetByCreatedBy(altId).OrderByDescending(o=>o.CreatedUtc).Take(10);
                return new DashBoardResponseViewModel
                {
                    RecentActivity = AutoMapper.Mapper.Map<List<RecentActivity>>(RecentActivity),
                    User = AutoMapper.Mapper.Map<User>(user)
                };
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}