using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Country.FactBook.Repositories;
using Web.Country.FactBook.ViewModels;

namespace Web.Country.FactBook.Controllers
{
    public class MenuController : Controller
    {
        private IFeatureRepository _featureRepository;
        private IRoleFeatureMappingRepository _roleFeatureMappingRepository;
        private IUserRepository _userRepository;
        public MenuController(IFeatureRepository featureRepository, IRoleFeatureMappingRepository roleFeatureMappingRepository, IUserRepository userRepository)
        {
            _featureRepository = featureRepository;
            _roleFeatureMappingRepository = roleFeatureMappingRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        [Route("api/authednavmenu/{altId}")]
        public ActionResult AuthedNavMenu(Guid altId)
        {
            Contracts.DataModels.User user = AutoMapper.Mapper.Map<Contracts.DataModels.User>(_userRepository.GetByAltId(altId));
            var features = _featureRepository.GetAll(null);
            var roleFeatures = _roleFeatureMappingRepository.GetByRoleId((int)user.RoleId);


            return PartialView("PartialViews/_AuthedNavMenu", new MenueResponseViewModel {
                Features = AutoMapper.Mapper.Map<List<Contracts.Models.Feature>>(features.Where(w => roleFeatures.Select(s => s.Id).Contains(w.Id))),
               User = AutoMapper.Mapper.Map<Contracts.Models.User>(user)
            });
        }
    }
}
