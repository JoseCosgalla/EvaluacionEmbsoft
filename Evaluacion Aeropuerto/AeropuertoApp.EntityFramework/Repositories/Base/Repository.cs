using System;
using System.Collections.Generic;

namespace AeropuertoApp.EntityFramework.Repositories.Base
{
    public class Repository<T> : Domain.Repositories.Base.IRepository<T>  where T : class
    {
        private readonly Database.IDataBaseSqlServerEntityFramework _database;

        public Repository(Database.IDataBaseSqlServerEntityFramework database)
        {
            _database = database;
        }

        public void Add(T item)
        {
            _database.Insert<T>(item);
        }

        public IEnumerable<T> FindAll()
        {
            return _database.GetAll<T>();
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _database.GetBy<T>(predicate);
        }

        public void Remove(T item)
        {
            _database.Remove<T>(item);
        }

        public void Update(T item)
        {
            _database.Update<T>(item);
        }
    }
}
