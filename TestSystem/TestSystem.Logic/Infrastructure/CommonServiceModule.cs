using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;
namespace TestSystem.Logic.Infrastructure
{
    public  class CommonServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommonService>().To<CommonService>();
        }
    }
}
