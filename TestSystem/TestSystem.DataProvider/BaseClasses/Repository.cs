using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TestSystem.DataProvider.Interfaces;
using System.Linq.Expressions;



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
        protected readonly DbContext context;

        public Repository(DbContext _context)
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
            context.Entry(entity).State = EntityState.Detached;
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
