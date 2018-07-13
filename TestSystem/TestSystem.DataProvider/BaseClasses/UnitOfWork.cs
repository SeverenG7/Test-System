using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.Repositories;
using TestSystem.DataProvider.ContextData;



namespace TestSystem.DataProvider.BaseClasses
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestContext context;

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


        public AnswerRepository Answers { get; private set; }
        public PropertyRepository Properties { get; private set; }
        public QuestionRepository Questions { get; private set; }
        public ResultRepository Results { get; private set; }
        public TestRepository Tests { get; private set; }
        public ThemeRepository Themes { get; private set; }
        public UserInfoRepository UserInfoes { get; private set; }
        public UserRepository Users { get; private set; }



        public int Complete() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}
