using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AeropuertoApp.Domain.Repositories.Base
{
    public interface IReadableRepository<T>
    {
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAll();
    }
}
