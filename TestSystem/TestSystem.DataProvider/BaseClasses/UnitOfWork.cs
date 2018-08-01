using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.Repositories;
using TestSystem.DataProvider.Context;
using TestSystem.Model.Models;
using System.Threading.Tasks;
using TestSystem.DataProvider.IdentityManager;
using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestSystem.DataProvider.BaseClasses
{

    /// <summary>
    /// Realization of IUnitOfWork interface
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationContext _context; 
        private bool _disposed = false;

        /// <summary>
        /// In this constructer we initialize all repositories.
        /// </summary>
        /// <param name="_context"></param>
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Answers = new AnswerRepository(_context);
            Questions = new QuestionRepository(_context);
            Results = new ResultRepository(_context);
            Tests = new TestRepository(_context);
            Themes = new ThemeRepository(_context);
            UserInfoes = new UserInfoRepository(_context);
            ApplicationRoleManagers = new ApplicationRoleManager
                (new RoleStore < ApplicationRole >(_context) );
            ApplicationUserManagers = new ApplicationUserManager
                (new UserStore<ApplicationUser>(_context));
        }

     

        public IRepository<Answer> Answers { get; private set; }
        public IRepository<Question> Questions { get; private set; }
        public IRepository<Result> Results { get; private set; }
        public IRepository<Test> Tests { get; private set; }
        public IRepository<Theme> Themes { get; private set; }
        public IRepository<UserInfo> UserInfoes { get; private set; }

        public ApplicationUserManager ApplicationUserManagers { get; private set; }
        public ApplicationRoleManager ApplicationRoleManagers { get; private set; }

        public int Complete() => _context.SaveChanges();


        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Answers.Dispose();
                    Questions.Dispose();
                    Results.Dispose();
                    Tests.Dispose();
                    Themes.Dispose();
                    UserInfoes.Dispose();
                    ApplicationRoleManagers.Dispose();
                    ApplicationRoleManagers.Dispose();
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
