using Ninject.Modules;
using TestSystem.DataProvider.BaseClasses;
using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.ContextData;

namespace TestSystem.Logic.Infrastructure
{
    public class UnitOfWorkModule : NinjectModule
    {
        private TestContext context = new TestContext();

        public override void Load()
        {
           Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context" ,context);
        }
    }
}
