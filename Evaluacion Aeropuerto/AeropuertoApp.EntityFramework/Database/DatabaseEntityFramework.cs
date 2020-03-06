using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AeropuertoApp.EntityFramework.Database
{
    public class DatabaseEntityFramework : IDataBaseSqlServerEntityFramework
    {
        private AppDbContext _dbContext;
        private DbContextTransaction _dbTransaction;


        #region Connection
        public void Open()
        {
            _dbContext = new AppDbContext();
            _dbContext.Database.Connection.Open();
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbTransaction.Commit();
            _dbTransaction.Dispose();
            _dbTransaction = null;
            _dbContext.Database.Connection.Close();
            _dbContext.Dispose();
            _dbContext = null;
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
            _dbTransaction.Dispose();
            _dbTransaction = null;
            _dbContext.Database.Connection.Close();
            _dbContext.Dispose();
            _dbContext = null;
        }

        public void Close()
        {
            if (_dbTransaction != null)
            {
                _dbTransaction.Rollback();
                _dbTransaction.Dispose();
                _dbTransaction = null;
            }
            if (_dbContext != null)
            {
                _dbContext.Database.Connection.Close();
                _dbContext.Dispose();
                _dbContext = null;
            }
        }
        #endregion

        public int CountBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _dbContext.Set<T>().Count(predicate);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>().ToList();
        }

        public IEnumerable<T> GetBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public T GetFirstBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public void Insert<T>(T objectToInsert) where T : class
        {
            _dbContext.Set<T>().Add(objectToInsert);
            _dbContext.SaveChanges();
        }

        public void Remove<T>(T objectToRemove) where T : class
        {
            if (objectToRemove is Domain.IDeletable)
            {
                var table = objectToRemove as Domain.IDeletable;
                table.IsDelete = true;
            }
            else
            {
                _dbContext.Set<T>().Remove(objectToRemove);
            }
            _dbContext.SaveChanges();
        }

        public void Update<T>(T objectToUpdate) where T : class
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<T> GetQueryable<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
