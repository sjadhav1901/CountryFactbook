using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface ICountryCurrencyMappingRepository : IOrmRepository<CountryCurrencyMapping>
    {
        CountryCurrencyMapping GetByContryAndCurrencyId(int countryId, int currencyId);
    }

    public class CountryCurrencyMappingRepository : OrmRepository<CountryCurrencyMapping>, ICountryCurrencyMappingRepository
    {
        public CountryCurrencyMappingRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public CountryCurrencyMapping GetByContryAndCurrencyId(int countryId, int currencyId)
        {
            return GetAll(s => s.Where($"{nameof(CountryCurrencyMapping.CountryId):C}=@CountryId AND {nameof(CountryCurrencyMapping.CurrencyId):C} = @CurrencyId")
           .WithParameters(new { CountryId = countryId, CurrencyId = currencyId })
           ).FirstOrDefault();
        }
    }
}
