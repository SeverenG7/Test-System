using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;

namespace TestSystem.Logic.Infrastructure
{
    public class UserServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
        }
    }
}
