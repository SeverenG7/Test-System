using System;
using System.Web.Mvc;
using Ninject.Modules;
using TestSystem.DataProvider.BaseClasses;
using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.ContextData;
using Ninject;


namespace TestSystem.Logic.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private TestContext context = new TestContext();

        public override void Load()
        {
           Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context" ,context);
        }
    }
}
