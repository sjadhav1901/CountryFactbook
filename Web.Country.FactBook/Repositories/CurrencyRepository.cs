using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface ICurrencyRepository : IOrmRepository<Currency>
    {
        Currency GetByCode(string code);
    }

    public class CurrencyRepository : OrmRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public Currency GetByCode(string code)
        {
            return GetAll(s => s.Where($"{nameof(Currency.Code):C} = @Code AND IsEnabled=1")
                .WithParameters(new { Code = code })
            ).FirstOrDefault();
        }
    }
}
