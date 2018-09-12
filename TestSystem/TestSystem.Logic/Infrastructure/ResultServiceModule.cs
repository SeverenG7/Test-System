using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;
namespace TestSystem.Logic.Infrastructure
{
    public class ResultServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IResultService>().To<ResultService>();
        }
    }
}
