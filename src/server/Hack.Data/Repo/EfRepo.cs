using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Nensure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hack.Data
{
    public class EfRepo<T> : IEntityRepo<T> where T : class
    {
        protected readonly HackContext _context;

        protected DbSet<T> Set => _context.Set<T>();

        public EfRepo(IContextFactory factory)
        {
            Ensure.NotNull(factory);
            _context = factory.Create();
        }

        public T Create(T entity)
        {
            Ensure.NotNull(entity);
            Set.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            Ensure.NotNull(entity);
            Set.Remove(entity);
        }

        public T Find(params object[] keys)
        {
            Ensure.NotNull(keys);
            return Set.Find(keys);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate = null)
        {
            return predicate is null ? Set.ToArray() : Set.Where(predicate).ToArray();
        }

        public void Update(T entity)
        {
            Ensure.NotNull(entity);
            Set.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}