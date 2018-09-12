using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;

namespace TestSystem.Logic.Infrastructure
{
    public class ThemeServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IThemeService>().To<ThemeService>();
        }
    }
}
