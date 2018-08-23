using TestSystem.Model.Models;
using TestSystem.DataProvider.Context;
using TestSystem.DataProvider.BaseClasses;

namespace TestSystem.DataProvider.Repositories
{
    public class ThemeRepository : Repository<Theme>
    {
        public ThemeRepository(ApplicationContext context) : base(context)
        { }
    }
}

