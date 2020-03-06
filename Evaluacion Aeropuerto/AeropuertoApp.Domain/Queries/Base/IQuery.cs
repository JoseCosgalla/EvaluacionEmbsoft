using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AeropuertoApp.Domain.Queries.Base
{
    public interface IQuery<T>
    {
        void Init();
        void AddWhere(Expression<Func<T, bool>> predicate);
        void IncludeObject(Expression<Func<T, object>> predicate);
        void Sort(string sort, string sortBy);
        void Paginate(int itemsToShow, int page);
        int TotalRecords();
        IEnumerable<T> Execute();
    }
}