using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Web.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

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
            UnitOfWorkModule unitOfWorkModule = new UnitOfWorkModule();
            QuestionServiceModule questionServiceModule = new QuestionServiceModule();
            AnswerServiceModule answerServiceModule = new AnswerServiceModule();
            ThemeServiceModule themeServiceModule = new ThemeServiceModule();
            _kernel.Load(unitOfWorkModule, testServiceModule,questionServiceModule,
                answerServiceModule, themeServiceModule);
        }

    }
}