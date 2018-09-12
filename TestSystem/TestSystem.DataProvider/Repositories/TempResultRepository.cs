using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public class TempResultRepository : Repository<TempResult>
    {
        public TempResultRepository(ApplicationContext context) : base(context)
        { }
    }
}
