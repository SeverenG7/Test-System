using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;

namespace TestSystem.Web.Util
{
    public class TestModulecs : NinjectModule
    {
        public override void Load()
        {
            Bind<ITestService>().To<TestService>();
        }
    }
}