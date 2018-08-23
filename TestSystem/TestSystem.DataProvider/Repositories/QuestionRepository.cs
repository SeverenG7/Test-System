using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{

    public class QuestionRepository : Repository<Question>
    {
        public QuestionRepository(ApplicationContext context) : base(context)
        { }

    }
}
