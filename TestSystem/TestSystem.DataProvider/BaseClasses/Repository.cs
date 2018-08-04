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
            context.Entry(entity).State = EntityState.Detached;
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }


        public void AddNewTest(TestSystem.Model.Models.Test test)
        {
            var testName = new SqlParameter("@TestName", test.TestName);
            var numberQuestions = new SqlParameter("@QuestionsNumber", test.QuestionsNumber);
            var description = new SqlParameter("@TestDescription", test.TestDescription);
            var date = new SqlParameter("@CreateDate", test.CreateDate);
            var theme = new SqlParameter("@IdTheme", test.IdTheme);
            var difficult= new SqlParameter("@Difficult", test.Difficult);

 
            context.Database.ExecuteSqlCommand("Exec AddTest @TestName , @QuestionsNumber , @TestDescription," +
                "@CreateDate ,@IdTheme ,@Difficult ",
                testName, numberQuestions, description, date, theme, difficult);

            
        }

        public void AddQuestionsToTest(TestSystem.Model.Models.Test test , List<int> idQuestions)
        {

            var IdTest = new SqlParameter("@IdTest", test.IdTest);
            foreach (int id in idQuestions)
            {
                var IdQuestion = new SqlParameter("@IdQuestion", id);
                context.Database.ExecuteSqlCommand("Exec InsertQuestion @IdTest , @IdQuestion",
                    IdTest, IdQuestion);
            }
            context.SaveChanges();
        }
    }
}
