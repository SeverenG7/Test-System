using Ninject.Modules;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Services;

namespace TestSystem.Logic.Infrastructure
{
    public class QuestionServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQuestionService>().To<QuestionService>();
        }
    }
}