using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AeropuertoApp.Domain.Queries.Base;
using AeropuertoApp.EntityFramework.Database;
using AeropuertoApp.Infrastructure.Queries;

namespace AeropuertoApp.EntityFramework.Queries
{
    public class QueryBase<T> : IQuery<T> where T : class
    {
        private readonly IDataBaseSqlServerEntityFramework _dataBaseSqlServerEntityFramework;
        public IQueryable<T> Query;

        public QueryBase(IDataBaseSqlServerEntityFramework dataBaseSqlServerEntityFramework)
        {
            _dataBaseSqlServerEntityFramework = dataBaseSqlServerEntityFramework;
        }
        public void Init()
        {
            Query = _dataBaseSqlServerEntityFramework.GetQueryable<T>();
        }

        public void Sort(string sort, string sortBy)
        {
            Query = QueryFactory.SortByPropertyResolver(sort, sortBy, Query);
        }

        public void Paginate(int itemsToShow, int page)
        {
            Query = QueryFactory.PaginationResolver(itemsToShow, page, Query);
        }

        public void AddWhere(Expression<Func<T, bool>> predicate)
        {
            Query = Query.Where(predicate);
        }

        public void IncludeObject(Expression<Func<T, object>> predicate)
        {
            Query = Query.Include(predicate);
        }

        public int TotalRecords()
        {
            return Query.Count();
        }

        public IEnumerable<T> Execute()
        {
            return Query.ToList();
        }
    }
}