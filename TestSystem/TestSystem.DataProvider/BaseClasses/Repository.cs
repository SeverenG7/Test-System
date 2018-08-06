using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TestSystem.DataProvider.Interfaces;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;

namespace TestSystem.DataProvider.BaseClasses
{

    /// <summary>
    /// Gemeric realization of IRepository interface.
    /// Give opportunity to realization all simple methods in one place, don't duplicate code for this
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns>TEntity here is a like generic type of any data objcet. </returns>
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        /// <summary>
        /// Also here use generic DbContext, for bigger agility
        /// </summary>
        protected readonly IdentityDbContext context;

        public Repository(IdentityDbContext _context)
        {
            context = _context;
        }
        /// <summary>
        /// More information for all this group of methods you can find in IRepository.
        /// </summary>
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

        public void Updating(TEntity entity)
        {
            string str = context.Entry(entity).State.ToString();
            str = str + "";
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }


        public void Dispose()
        {
            context.Dispose();
        }

    }
}
