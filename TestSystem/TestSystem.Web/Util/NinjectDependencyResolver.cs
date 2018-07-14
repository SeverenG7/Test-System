using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using TestSystem.Logic.Services;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Web.Util
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
            ServiceModule serviceModule = new ServiceModule();
            kernel.Load(serviceModule);
            kernel.Bind<ITestService>().To<TestService>();

        }

    }
}