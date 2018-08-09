using Ninject.Modules;
using TestSystem.DataProvider.BaseClasses;
using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.Context;

namespace TestSystem.Logic.Infrastructure
{
    public class UnitOfWorkModule : NinjectModule
    {
        private readonly ApplicationContext _context = new ApplicationContext();

        public override void Load()
        {
           Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context" ,_context);
        }
    }
}
