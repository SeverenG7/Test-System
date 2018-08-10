using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;

namespace TestSystem.Logic.Infrastructure
{
    public class TestPassServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITestPassService>().To<TestPassService>();
        }
    }
}
