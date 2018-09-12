using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;

namespace TestSystem.Logic.Infrastructure
{
    public class TestServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITestService>().To<TestService>();
        }
    }
}
