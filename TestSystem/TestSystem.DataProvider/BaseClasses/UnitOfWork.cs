using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.Repositories;
using TestSystem.DataProvider.ContextData;
using TestSystem.Model.Models;



namespace TestSystem.DataProvider.BaseClasses
{

    /// <summary>
    /// Realization of IUnitOfWork interface
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly TestContext context;

        /// <summary>
        /// In this constructer we initialize all repositories.
        /// </summary>
        /// <param name="_context"></param>
        public UnitOfWork(TestContext _context)
        {
            context = _context;
            Answers = new AnswerRepository(context);
            Properties = new PropertyRepository(context);
            Questions = new QuestionRepository(context);
            Results = new ResultRepository(context);
            Tests = new TestRepository(context);
            Themes = new ThemeRepository(context);
            UserInfoes = new UserInfoRepository(context);
            Users = new UserRepository(context);
        }

     

        public IRepository<Answer> Answers { get; private set; }
        public IRepository<Property> Properties { get; private set; }
        public IRepository<Question> Questions { get; private set; }
        public IRepository<Result> Results { get; private set; }
        public IRepository<Test> Tests { get; private set; }
        public IRepository<Theme> Themes { get; private set; }
        public IRepository<UserInfo> UserInfoes { get; private set; }
        public IRepository<UserSystem> Users { get; private set; }

        public int Complete() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}
