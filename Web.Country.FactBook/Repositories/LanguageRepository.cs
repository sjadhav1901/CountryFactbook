using Contracts.DataModels;
using Db.Core.Repositories;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Country.FactBook.Repositories
{
    public interface ILanguageRepository : IOrmRepository<Language>
    {
        Language GetByName(string name);
    }

    public class LanguageRepository : OrmRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(IDataSettings dataSettings) : base(dataSettings)
        {
        }

        public Language GetByName(string name)
        {
            return GetAll(s => s.Where($"{nameof(Language.Name):C} = @Name AND IsEnabled=1")
                .WithParameters(new { Name = name })
            ).FirstOrDefault();
        }
    }
}
