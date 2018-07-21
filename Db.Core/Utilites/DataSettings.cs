using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Db.Core.Utilites
{
    public interface IDataSettings :IDisposable
    {
        SqlDatabase TransactionalDatabase { get; }
        SqlDatabase ReadOnlyDatabase { get; }
        IUnitOfWork UnitOfWork { get; }
    }

    public class DataSettings : IDataSettings
    {
        private readonly IConfiguration _configurationRoot;
        private SqlDatabase _database;

        public DataSettings(IConfiguration configurationRoot)
        {
            _configurationRoot = configurationRoot;
            UnitOfWork = new UnitOfWork(this);
        }

        public string DatabaseConnectionString => _configurationRoot["ConnectionString"];
        public SqlDatabase TransactionalDatabase => _database ?? (_database = new SqlDatabase(DatabaseConnectionString));
        public SqlDatabase ReadOnlyDatabase => _database ?? (_database = new SqlDatabase(DatabaseConnectionString));
        public IUnitOfWork UnitOfWork { get; private set; }

        public void Dispose()
        {
            UnitOfWork = null;
        }
    }
}
