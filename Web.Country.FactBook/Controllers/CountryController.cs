using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Country.FactBook.Helpers;
using Web.Country.FactBook.Repositories;
using Web.Country.FactBook.ViewModels;

namespace Web.Country.FactBook.Controllers
{
    public class CountryController : Controller
    {
        private ICountryRepository _countryRepository;
        private ICurrencyRepository _currencyRepository;
        private ILanguageRepository _languageRepository;
        private ICountryLanguageMappingRepository _countryLanguageMappingRepository;
        private ICountryCurrencyMappingRepository _countryCurrencyMappingRepository;
        private IActivityHelper _activityHelper;
        private IFavouriteRepository _favouriteRepository;
        public CountryController(ICountryRepository countryRepository, ICurrencyRepository currencyRepository, ILanguageRepository languageRepository, ICountryLanguageMappingRepository countryLanguageMappingRepository, ICountryCurrencyMappingRepository countryCurrencyMappingRepository, IActivityHelper activityHelper, IFavouriteRepository favouriteRepository)
        {
            _countryRepository = countryRepository;
            _currencyRepository = currencyRepository;
            _languageRepository = languageRepository;
            _countryLanguageMappingRepository = countryLanguageMappingRepository;
            _countryCurrencyMappingRepository = countryCurrencyMappingRepository;
            _activityHelper = activityHelper;
            _favouriteRepository = favouriteRepository;
        }

        public IActionResult SearchCountry()
        {
            return View();
        }

        [HttpGet]
        [Route("api/search/countries/{name}")]
        public List<Contracts.Models.Country> GetAllCountries(string name)
        {
            try
            {
                List<Contracts.Models.Country> country = AutoMapper.Mapper.Map<List<Contracts.Models.Country>>(_countryRepository.GetAll(null)).ToList();
                return country;
            }
            catch (Exception)
            {
            }
            return null;
        }

        [HttpGet]
        [Route("api/get/countries/{altId}")]
        public CountryDetailResponseViewModel GetCountryDetails(Guid altId)
        {
            try
            {
                CountryDetailResponseViewModel countryCMSResponseViewModel = new CountryDetailResponseViewModel();
                Contracts.Models.Country country = AutoMapper.Mapper.Map<Contracts.Models.Country>(_countryRepository.GetByAltId(altId));
                List<Contracts.Models.Currency> currencies = AutoMapper.Mapper.Map<List<Contracts.Models.Currency>>(_currencyRepository.GetAll(null).ToList());
                List<Contracts.Models.Language> languages = AutoMapper.Mapper.Map<List<Contracts.Models.Language>>(_languageRepository.GetAll(null).ToList());
                List<Contracts.Models.CountryLanguageMapping> countryLanguageMappings = AutoMapper.Mapper.Map<List<Contracts.Models.CountryLanguageMapping>>(_countryLanguageMappingRepository.GetAll(null).Where(w => w.CountryId == country.Id).ToList());
                List<Contracts.Models.CountryCurrencyMapping> countryCurrencyMappings = AutoMapper.Mapper.Map<List<Contracts.Models.CountryCurrencyMapping>>(_countryCurrencyMappingRepository.GetAll(null).Where(w => w.CountryId == country.Id).ToList());
                return new CountryDetailResponseViewModel
                {
                    Country = country,
                    Currencies = currencies,
                    Languages = languages,
                    CountryCurrencyMappings = countryCurrencyMappings,
                    CountryLanguageMappings = countryLanguageMappings
                };
            }
            catch (Exception)
            {
            }
            return null;
        }

        [HttpPost]
        [Route("/api/favorite")]
        public Contracts.DataModels.Favourite Favourites([FromBody] Favourite model)
        {
            try
            {
                Contracts.Models.Country country = AutoMapper.Mapper.Map<Contracts.Models.Country>(_countryRepository.GetByAltId(model.CountryAltId));
                Contracts.DataModels.Favourite favourite = _favouriteRepository.GetByCountryIdAndCreatedBy(model.CountryAltId, model.CreatedBy);
                if (favourite == null)
                {
                    favourite = _favouriteRepository.Save(new Contracts.DataModels.Favourite
                    {
                        CountryAltId = model.CountryAltId,
                        IsEnabled = true,
                        CreatedUtc = DateTime.UtcNow,
                        CreatedBy = model.CreatedBy
                    });
                    _activityHelper.SaveActivity("Added to Favourites", "You have added country " + country.Name + " on " + DateTime.UtcNow.ToString() + " (GMT)", model.CreatedBy);
                }
                return favourite;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}