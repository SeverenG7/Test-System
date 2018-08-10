using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TestSystem.DataProvider.Interfaces;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestSystem.DataProvider.BaseClasses
{

    /// <summary>
    /// Gemeric realization of IRepository interface.
    /// Give opportunity to realization all simple methods in one place, don't duplicate code for this
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns>TEntity here is a like generic type of any data objcet. </returns>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Also here use generic DbContext, for bigger agility
        /// </summary>
        protected readonly IdentityDbContext Context;

        public Repository(IdentityDbContext context)
        {
            Context = context;
        }
        /// <summary>
        /// More information for all this group of methods you can find in IRepository.
        /// </summary>
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
