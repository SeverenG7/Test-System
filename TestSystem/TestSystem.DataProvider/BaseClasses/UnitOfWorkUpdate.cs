using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.Repositories;
using TestSystem.DataProvider.Context;
using TestSystem.Model.Models;
using System.Threading.Tasks;
using System;

namespace TestSystem.DataProvider.BaseClasses
{
    public class UnitOfWorkUpdate : IUnitOfWorkUpdate
    {

        private readonly ApplicationContext _context;
        private bool _disposed = false;

        /// <summary>
        /// In this constructer we initialize all repositories.
        /// </summary>
        /// <param name="_context"></param>
        public UnitOfWorkUpdate(ApplicationContext context)
        {
            _context = context;
            Questions = new QuestionRepository(_context);
            Tests = new TestRepository(_context);
            Themes = new ThemeRepository(_context);
        }

       
        public IRepository<Question> Questions { get; private set; }

        public IRepository<Test> Tests { get; private set; }
        public IRepository<Theme> Themes { get; private set; }

        public int Complete() => _context.SaveChanges();


        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Questions.Dispose();
                    Tests.Dispose();
                    Themes.Dispose();
                }
                this._disposed = true;

            }

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {

            await _context.SaveChangesAsync();
        }
    }
}


