using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface ICountryLanguageMappingRepository : IOrmRepository<CountryLanguageMapping>
    {
        CountryLanguageMapping GetByContryAndLanguageId(int countryId, int languageId);
    }

    public class CountryLanguageMappingRepository : OrmRepository<CountryLanguageMapping>, ICountryLanguageMappingRepository
    {
        public CountryLanguageMappingRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public CountryLanguageMapping GetByContryAndLanguageId(int countryId, int languageId)
        {
            return GetAll(s => s.Where($"{nameof(CountryLanguageMapping.CountryId):C}=@CountryId AND {nameof(CountryLanguageMapping.LanguageId):C} = @LanguageId")
           .WithParameters(new { CountryId = countryId, LanguageId = languageId })
           ).FirstOrDefault();
        }
    }
}
