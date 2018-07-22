using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface IRecentActivityRepository : IOrmRepository<RecentActivity>
    {
        IEnumerable<Contracts.DataModels.RecentActivity> GetByCreatedBy(Guid altId);
    }

    public class RecentActivityRepository : OrmRepository<RecentActivity>, IRecentActivityRepository
    {
        public RecentActivityRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public IEnumerable<RecentActivity> GetByCreatedBy(Guid altId)
        {
            return GetAll(s => s.Where($"{nameof(RecentActivity.CreatedBy):C} = @AltId")
                .WithParameters(new { AltId = altId })
            );
        }
    }
}
