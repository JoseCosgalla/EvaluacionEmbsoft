using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeropuertoApp.Domain.Repositories.Base
{
    public interface IRepository<T> : IReadableRepository<T>, IWritableRepository<T>
    {
    }
}
