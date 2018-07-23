using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface ICountryRepository : IOrmRepository<Contracts.DataModels.Country>
    {
        Contracts.DataModels.Country GetByName(string name);
        Contracts.DataModels.Country GetByAltId(Guid altId);
        IEnumerable<Contracts.DataModels.Country> GetByAltIds(IEnumerable<Guid> altId);
    }

    public class CountryRepository : OrmRepository<Contracts.DataModels.Country>, ICountryRepository
    {
        public CountryRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public Contracts.DataModels.Country GetByName(string name)
        {
            return GetAll(s => s.Where($"{nameof(Contracts.DataModels.Country.Name):C} = @Name AND IsEnabled=1")
                .WithParameters(new { Name = name })
            ).FirstOrDefault();
        }

        public Contracts.DataModels.Country GetByAltId(Guid altId)
        {
            return GetAll(s => s.Where($"{nameof(Contracts.DataModels.Country.AltId):C} = @AltId AND IsEnabled=1")
                .WithParameters(new { AltId = altId })
            ).FirstOrDefault();
        }

        public IEnumerable<Contracts.DataModels.Country> GetByAltIds(IEnumerable<Guid> altId)
        {
            return GetAll(s => s.Where($"{nameof(Contracts.DataModels.Country.AltId):C} IN @AltId AND IsEnabled=1")
                .WithParameters(new { AltId = altId })
            );
        }
    }
}
