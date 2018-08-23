using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{

    public class AnswerRepository : Repository<Answer>
    {
        public AnswerRepository(ApplicationContext context) : base(context)
        { }

    }
}
