using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.Repositories;
using TestSystem.DataProvider.Context;
using TestSystem.Model.Models;
using System.Threading.Tasks;
using TestSystem.DataProvider.IdentityManagers;
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
        private bool _disposed;

        /// <summary>
        /// In this constructer we initialize all repositories.
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            _context.Configuration.AutoDetectChangesEnabled = false;
            Answers = new AnswerRepository(_context);
            Questions = new QuestionRepository(_context);
            Results = new ResultRepository(_context);
            Tests = new TestRepository(_context);
            Themes = new ThemeRepository(_context);
            UserInfoes = new UserInfoRepository(_context);
            TempResults = new TempResultRepository(_context);
            UserQuestions = new UserQuestionsRepository(_context);
            UserAnswers = new UserAnswersRepository(_context);
            ApplicationRoleManagers = new ApplicationRoleManager
                (new RoleStore<ApplicationRole>(_context));
            ApplicationUserManagers = new ApplicationUserManager
                (new UserStore<ApplicationUser>(_context));
        }

        public IRepository<Answer> Answers { get; }
        public IRepository<Question> Questions { get; }
        public IRepository<Result> Results { get; }
        public IRepository<Test> Tests { get; }
        public IRepository<Theme> Themes { get; }
        public IRepository<UserInfo> UserInfoes { get; }
        public IRepository<TempResult> TempResults { get; }
        public IRepository<UserQuestion> UserQuestions { get; }
        public IRepository<UserAnswer> UserAnswers { get; }

        public ApplicationUserManager ApplicationUserManagers { get; }
        public ApplicationRoleManager ApplicationRoleManagers { get; }

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
                    TempResults.Dispose();
                    UserQuestions.Dispose();
                    UserAnswers.Dispose();
                    ApplicationRoleManagers.Dispose();
                    ApplicationRoleManagers.Dispose();
                }
                _disposed = true;

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
