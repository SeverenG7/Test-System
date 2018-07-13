using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TestSystem.DataProvider.Interfaces;
using System.Linq.Expressions;

namespace TestSystem.DataProvider.BaseClasses
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;

        public Repository(DbContext _context)
        {
            context = _context;
        }

        public TEntity Get(int id) => context.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> GetAll() => context.Set<TEntity>().ToList();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => context.Set<TEntity>().AddRange(entities);

        public void Remove(TEntity entity) => context.Set<TEntity>().Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => context.Set<TEntity>().RemoveRange(entities);



    }
}
