using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TestSystem.DataProvider.Interfaces;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestSystem.DataProvider.BaseClasses
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly IdentityDbContext Context;

        public Repository(IdentityDbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id) => Context.Set<TEntity>().Find(id);

        public IEnumerable<TEntity> GetAll() => Context.Set<TEntity>().ToList();

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }
 
        public void Add(TEntity entity) => Context.Set<TEntity>().Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().AddRange(entities);

        public void Remove(TEntity entity) => Context.Set<TEntity>().Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => Context.Set<TEntity>().RemoveRange(entities);

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
