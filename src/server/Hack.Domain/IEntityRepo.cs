using System;
using System.Collections.Generic;

namespace Hack.Domain
{
    public interface IEntityRepo<T> where T : class
    {
        T Create(T entity);

        void Delete(T entity);

        void Update(T entity);

        T Find(params object[] keys);

        IEnumerable<T> Get(Func<T, bool> predicate = null);

        void Save();
    }
}