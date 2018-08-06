using Ninject.Modules;
using TestSystem.DataProvider.BaseClasses;
using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.Context;

namespace TestSystem.Logic.Infrastructure
{
    public class UpdateModule: NinjectModule
    {
        private ApplicationContext _context = new ApplicationContext();

        public override void Load()
        {
            Bind<IUnitOfWorkUpdate>().To<UnitOfWorkUpdate>().WithConstructorArgument("context", _context);
        }
    }
}