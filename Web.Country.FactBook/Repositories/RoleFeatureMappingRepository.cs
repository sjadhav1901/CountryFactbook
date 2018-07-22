using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface IRoleFeatureMappingRepository : IOrmRepository<RoleFeatureMapping>
    {
        IEnumerable<RoleFeatureMapping> GetByRoleId(int roleId);
    }

    public class RoleFeatureMappingRepository : OrmRepository<RoleFeatureMapping>, IRoleFeatureMappingRepository
    {
        public RoleFeatureMappingRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public IEnumerable<RoleFeatureMapping> GetByRoleId(int roleId)
        {
            return GetAll(s => s.Where($"{nameof(RoleFeatureMapping.RoleId):C}=@RoleId AND IsEnabled=1")
           .WithParameters(new { RoleId = roleId })
           );
        }
    }
}
