using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public class TestRepository : Repository<Test>
    {
        public TestRepository(ApplicationContext context) : base(context)
        { }
    }
}
