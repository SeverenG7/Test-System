using TestSystem.Logic.Services;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestPassService
    {
        QuestionDto StartTest(int IdResult);
        TestPassService.TimerModule Timer { get; set; }
        OperationDetails UserPassingTest(string nameUser);
        QuestionDto TestPassing(int IdResult);
    }
}
