using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface IUserRepository: IOrmRepository<User>
    {
    }

    public class UserRepository : OrmRepository<User>, IUserRepository
    {
        public UserRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public User Get(int id)
        {
            return Get(new User { Id = id });
        }

    }
}
