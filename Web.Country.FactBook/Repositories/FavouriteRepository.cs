using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace Web.Country.FactBook.Repositories
{
    public interface IFavouriteRepository : IOrmRepository<Favourite>
    {
        Favourite GetByCountryIdAndCreatedBy(Guid countryAltId, Guid altId);
        IEnumerable<Favourite> GetByCreatedBy(Guid altId);
    }

    public class FavouriteRepository : OrmRepository<Favourite>, IFavouriteRepository
    {
        public FavouriteRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public Favourite GetByCountryIdAndCreatedBy(Guid countryAltId, Guid altId)
        {
            return GetAll(s => s.Where($"{nameof(Favourite.CountryAltId):C}=@CountryAltId AND {nameof(Favourite.CreatedBy):C} = @AltId")
           .WithParameters(new { CountryAltId = countryAltId, AltId = altId })
           ).FirstOrDefault();
        }

        public IEnumerable<Favourite> GetByCreatedBy(Guid altId)
        {
            return GetAll(s => s.Where($"{nameof(Favourite.CreatedBy):C} = @AltId")
           .WithParameters(new { AltId = altId })
           );
        }
    }
}
