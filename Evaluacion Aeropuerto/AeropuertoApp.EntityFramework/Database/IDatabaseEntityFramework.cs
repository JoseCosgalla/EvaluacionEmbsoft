using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AeropuertoApp.EntityFramework.Database
{
    public interface IDataBaseSqlServerEntityFramework
    {
        void Insert<T>(T objectToInsert) where T : class;
        void Update<T>(T objectToUpdate) where T : class;
        void Remove<T>(T objectToRemove) where T : class;
        T GetFirstBy<T>(Expression<Func<T, bool>> predicate) where T : class;
        IEnumerable<T> GetBy<T>(Expression<Func<T, bool>> predicate) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        int CountBy<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Open();
        void Commit();
        void Rollback();
        IQueryable<T> GetQueryable<T>() where T : class;
    }
}
