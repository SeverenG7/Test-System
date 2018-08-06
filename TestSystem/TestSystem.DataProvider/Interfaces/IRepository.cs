using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TestSystem.DataProvider.Interfaces
{
    /// <summary>
    /// IRepository interface for abstract access to data objects.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns>TEntity here is a like generic type of any data objcet. </returns>
    public interface IRepository<TEntity> : IDisposable where TEntity : class 
    {
        /// <summary>
        /// Group of methods to finding objects.
        /// </summary>
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// Group of methods to adding objects.
        /// </summary>
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Group of methods to removing objects.
        /// </summary>
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Updating(TEntity entity);
        void Update(TEntity entity);

    }
}
