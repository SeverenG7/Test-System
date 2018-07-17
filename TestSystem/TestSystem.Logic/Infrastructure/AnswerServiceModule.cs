using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;

namespace TestSystem.Logic.Infrastructure
{
    public class AnswerServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAnswerService>().To<AnswerService>();
        }
    }
}
