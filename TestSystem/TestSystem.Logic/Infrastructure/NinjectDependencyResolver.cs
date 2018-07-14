/*using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using TestSystem.DataProvider.BaseClasses;
using TestSystem.DataProvider.Interfaces;
using TestSystem.DataProvider.ContextData;

namespace TestSystem.Logic.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            TestContext context = new TestContext();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context", context);        }
    }
}*/
