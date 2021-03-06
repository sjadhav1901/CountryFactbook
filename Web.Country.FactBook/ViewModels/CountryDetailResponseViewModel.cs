﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.ViewModels
{
    public class CountryDetailResponseViewModel
    {
        public Contracts.Models.Country Country { get; set; }
        public List<Contracts.Models.Currency> Currencies { get; set; }
        public List<Contracts.Models.Language> Languages { get; set; }
        public List<Contracts.Models.CountryLanguageMapping> CountryLanguageMappings { get; set; }
        public List<Contracts.Models.CountryCurrencyMapping> CountryCurrencyMappings { get; set; }
    }
}
