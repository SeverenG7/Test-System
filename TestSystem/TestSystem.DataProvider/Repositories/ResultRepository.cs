using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public class ResultRepository : Repository<Result>
    {
        public ResultRepository(ApplicationContext context) : base(context)
        { }

    }
}

