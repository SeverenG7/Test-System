using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
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
            TestServiceModule testServiceModule = new TestServiceModule();
            UnitOfWorkModule unitOfWorkModule = new UnitOfWorkModule();
            QuestionServiceModule questionServiceModule = new QuestionServiceModule();
            AnswerServiceModule answerServiceModule = new AnswerServiceModule();
            ThemeServiceModule themeServiceModule = new ThemeServiceModule();
            UserServiceModule userServiceModule = new UserServiceModule();
            UpdateModule updateModule = new UpdateModule();
            ResultServiceModule resultServiceModule = new ResultServiceModule();

            kernel.Load(unitOfWorkModule, testServiceModule,questionServiceModule,
                answerServiceModule, themeServiceModule , userServiceModule ,updateModule, 
                resultServiceModule);

        }

    }
}