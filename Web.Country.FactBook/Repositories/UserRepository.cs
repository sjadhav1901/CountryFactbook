using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface IUserRepository : IOrmRepository<User>
    {
        User GetByEmail(string email);
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

        public User GetByEmail(string email)
        {
            return GetAll(s => s.Where($"{nameof(User.Email):C} = @Email AND IsEnabled=1")
                .WithParameters(new { Email = email })
            ).FirstOrDefault();
        }
    }
}
