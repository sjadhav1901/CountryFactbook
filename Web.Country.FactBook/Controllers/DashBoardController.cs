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
        private IFavouriteRepository _favouriteRepository;
        private ICountryRepository _countryRepository;
        public DashBoardController(IRecentActivityRepository recentActivityRepository, IUserRepository userRepository, IFavouriteRepository favouriteRepository, ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
            _recentActivityRepository = recentActivityRepository;
            _userRepository = userRepository;
            _favouriteRepository = favouriteRepository;
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
                IEnumerable<Contracts.DataModels.Favourite> Favourite = _favouriteRepository.GetByCreatedBy(altId).OrderByDescending(o => o.CreatedUtc).Take(10);
                IEnumerable<Contracts.Models.Country> country = AutoMapper.Mapper.Map<IEnumerable<Contracts.Models.Country>>(_countryRepository.GetByAltIds(Favourite.Select(s=>s.CountryAltId).Distinct()));
                return new DashBoardResponseViewModel
                {
                    RecentActivity = AutoMapper.Mapper.Map<List<RecentActivity>>(RecentActivity),
                    Country = country.ToList(),
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