using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public class UserQuestionsRepository : Repository<UserQuestion>
    {
        public UserQuestionsRepository(ApplicationContext context) : base(context)
        { }
    }
}
