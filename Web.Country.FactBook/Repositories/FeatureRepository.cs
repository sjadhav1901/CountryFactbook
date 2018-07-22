using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface IFeatureRepository : IOrmRepository<Feature>
    {
    }

    public class FeatureRepository : OrmRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }
    }
}
