using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Db.Core.Utilites;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Linq;
using Contracts.Interfaces;

namespace Db.Core.Repositories
{
    public interface IOrmRepository<T>
    {
        T Get(T obj);
        IEnumerable<T> GetAll(Func<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<T>, IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<T>> statement = null);
        bool Delete(T obj);
        T Save(T obj);
    }

    public abstract class OrmRepository<T> : IOrmRepository<T>
    {
        protected IDataSettings _dataSettings { get; }
        protected OrmRepository(IDataSettings dataSettings)
        {
            _dataSettings = dataSettings;
        }

        protected DbConnection GetCurrentConnection()
        {
            return _dataSettings.UnitOfWork.GetDbConnection();
        }

        protected DbTransaction GetCurrentTransaction()
        {
            return _dataSettings.UnitOfWork.GetDbTransaction();
        }

        public T Get(T obj)
        {
            return GetCurrentConnection().Get(obj, statement => statement.AttachToTransaction(GetCurrentTransaction()));
        }

        public IEnumerable<T> GetAll(Func<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<T>, IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<T>> statement = null)
        {
            if (statement == null)
            {
                statement = s => s;
            }

            var conn = GetCurrentConnection();
            var transaction = GetCurrentTransaction();
            return conn.Find<T>(s => statement(transaction != null ? s.AttachToTransaction(transaction) : s));
        }

        public bool Delete(T obj)
        {
            return GetCurrentConnection().Delete(obj, s => s.AttachToTransaction(GetCurrentTransaction()));
        }

        public T Save(T obj)
        {
            bool shouldInsert = CheckForInsert(obj);
            var conn = GetCurrentConnection();
            if (shouldInsert)
            {
                conn.Insert(obj, statement => statement.AttachToTransaction(GetCurrentTransaction()));
            }
            else
            {
                conn.Update(obj, statement => statement.AttachToTransaction(GetCurrentTransaction()));
            }
            return obj;
        }

        protected bool CheckForInsert(T obj)
        {
            return ((IId<int>) obj).Id == 0;
        }
    }
}
