using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DataModels;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Country.FactBook.ApiIntegrations;
using Web.Country.FactBook.Helpers;
using Web.Country.FactBook.Repositories;
using Web.Country.FactBook.ViewModels;

namespace Web.Country.FactBook.Controllers
{
    public class CMSController : Controller
    {
        private IApiCountryAll _apiCountryAll;
        private ICountryRepository _countryRepository;
        private ICurrencyRepository _currencyRepository;
        private ILanguageRepository _languageRepository;
        private ICountryLanguageMappingRepository _countryLanguageMappingRepository;
        private ICountryCurrencyMappingRepository _countryCurrencyMappingRepository;
        private IActivityHelper _activityHelper;
        public CMSController(IApiCountryAll apiCountryAll, ICountryRepository countryRepository, ICurrencyRepository currencyRepository, ILanguageRepository languageRepository, ICountryLanguageMappingRepository countryLanguageMappingRepository, ICountryCurrencyMappingRepository countryCurrencyMappingRepository, IActivityHelper activityHelper)
        {
            _apiCountryAll = apiCountryAll;
            _countryRepository = countryRepository;
            _currencyRepository = currencyRepository;
            _languageRepository = languageRepository;
            _countryLanguageMappingRepository = countryLanguageMappingRepository;
            _countryCurrencyMappingRepository = countryCurrencyMappingRepository;
            _activityHelper = activityHelper;
        }

        public IActionResult SyncCountries()
        {
            return View();
        }

        public IActionResult CountryListing()
        {
            return View();
        }

        [HttpPost]
        [Route("api/synccountries")]
        public bool SyncCountries([FromBody] Contracts.Models.User model)
        {
            List<Contracts.Models.ApiIntegrations.Country> countries = _apiCountryAll.GetAllCountries();
            foreach (var itemCountry in countries)
            {
                try
                {
                    Contracts.DataModels.Country country = _countryRepository.GetByName(itemCountry.name);
                    if (country == null)
                    {
                        country = _countryRepository.Save(new Contracts.DataModels.Country
                        {
                            AltId = Guid.NewGuid(),
                            Name = itemCountry.name,
                            AlphaTwoCode = itemCountry.alpha2Code,
                            AlphaThreeCode = itemCountry.alpha3Code,
                            Capital = itemCountry.capital,
                            Flags = itemCountry.flag,
                            TimeZone = itemCountry.timezones != null ? itemCountry.timezones[0] : null,
                            IsEnabled = true,
                            CreatedUtc = DateTime.UtcNow,
                            CreatedBy = model.AltId
                        });
                    }
                    foreach (var itemCurrency in itemCountry.currencies)
                    {
                        try
                        {
                            Contracts.DataModels.Currency currency = _currencyRepository.GetByCode(itemCurrency.code);
                            if (currency == null)
                            {
                                currency = _currencyRepository.Save(new Contracts.DataModels.Currency
                                {
                                    Name = itemCurrency.name,
                                    Code = itemCurrency.code,
                                    Symbol = itemCurrency.symbol,
                                    IsEnabled = true,
                                    CreatedUtc = DateTime.UtcNow,
                                    CreatedBy = model.AltId
                                });
                            }
                            Contracts.DataModels.CountryCurrencyMapping countryCurrencyMapping = _countryCurrencyMappingRepository.GetByContryAndCurrencyId(country.Id, currency.Id);
                            if (countryCurrencyMapping == null)
                            {
                                countryCurrencyMapping = _countryCurrencyMappingRepository.Save(new Contracts.DataModels.CountryCurrencyMapping
                                {
                                    CountryId = country.Id,
                                    CurrencyId = currency.Id,
                                    IsEnabled = true,
                                    CreatedUtc = DateTime.UtcNow,
                                    CreatedBy = model.AltId
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    foreach (var itemLanguage in itemCountry.languages)
                    {
                        try
                        {
                            Contracts.DataModels.Language language = _languageRepository.GetByName(itemLanguage.name);
                            if (language == null)
                            {
                                language = _languageRepository.Save(new Contracts.DataModels.Language
                                {
                                    Name = itemLanguage.name,
                                    IsEnabled = true,
                                    CreatedUtc = DateTime.UtcNow,
                                    CreatedBy = model.AltId
                                });
                            }
                            Contracts.DataModels.CountryLanguageMapping countryLanguageMapping = _countryLanguageMappingRepository.GetByContryAndLanguageId(country.Id, language.Id);
                            if (countryLanguageMapping == null)
                            {
                                countryLanguageMapping = _countryLanguageMappingRepository.Save(new Contracts.DataModels.CountryLanguageMapping
                                {
                                    CountryId = country.Id,
                                    LanguageId = language.Id,
                                    IsEnabled = true,
                                    CreatedUtc = DateTime.UtcNow,
                                    CreatedBy = model.AltId
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            _activityHelper.SaveActivity("Country Data Synchronization", "You have performed country data sync opertion on " + DateTime.UtcNow.ToString() + " (GMT)", model.AltId);
            return true;
        }

        [HttpGet]
        [Route("api/all/countries")]
        public CountryCMSResponseViewModel GetAllCountries()
        {
            try
            {
                CountryCMSResponseViewModel countryCMSResponseViewModel = new CountryCMSResponseViewModel();
                List<Contracts.Models.Country> countries = AutoMapper.Mapper.Map<List<Contracts.Models.Country>>(_countryRepository.GetAll(null).ToList());
                List<Contracts.Models.Currency> currencies = AutoMapper.Mapper.Map<List<Contracts.Models.Currency>>(_currencyRepository.GetAll(null).ToList());
                List<Contracts.Models.Language> languages = AutoMapper.Mapper.Map<List<Contracts.Models.Language>>(_languageRepository.GetAll(null).ToList());
                List<Contracts.Models.CountryLanguageMapping> countryLanguageMappings = AutoMapper.Mapper.Map<List<Contracts.Models.CountryLanguageMapping>>(_countryLanguageMappingRepository.GetAll(null).ToList());
                List<Contracts.Models.CountryCurrencyMapping> countryCurrencyMappings = AutoMapper.Mapper.Map<List<Contracts.Models.CountryCurrencyMapping>>(_countryCurrencyMappingRepository.GetAll(null).ToList());
                return new CountryCMSResponseViewModel {
                    Countries = countries,
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
    }
}