using TestSystem.Logic.Infrastructure;
using TestSystem.Logic.ViewModel;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestPassService
    {
        QuestionViewModel StartTest(int IdResult);
        OperationDetails TestPassing( QuestionViewModel question);
        OperationDetails GetCurrentTestState(int IdQuestion);
        OperationDetails Results();
    }
}
