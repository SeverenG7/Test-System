using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public  class UserAnswersRepository : Repository<UserAnswer>
    {
        public UserAnswersRepository(ApplicationContext context) : base(context)
        { }
    }
}
