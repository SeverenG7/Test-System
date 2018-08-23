using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Web.Infrasrtuctre
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            TestServiceModule testServiceModule = new TestServiceModule();
            CommonServiceModule commonServiceModule = new CommonServiceModule();
            UnitOfWorkModule unitOfWorkModule = new UnitOfWorkModule();
            QuestionServiceModule questionServiceModule = new QuestionServiceModule();
            ThemeServiceModule themeServiceModule = new ThemeServiceModule();
            UserServiceModule userServiceModule = new UserServiceModule();
            ResultServiceModule resultServiceModule = new ResultServiceModule();
            TestPassServiceModule testPassServiceModule = new TestPassServiceModule();

            _kernel.Load(unitOfWorkModule, testServiceModule,questionServiceModule,
                themeServiceModule , userServiceModule ,
                resultServiceModule ,testPassServiceModule, commonServiceModule);

        }

    }
}