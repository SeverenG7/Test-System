using TestSystem.Logic.Services;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestPassService
    {
        QuestionDto StartTest(int IdResult);
        OperationDetails UserPassingTest(string nameUser);
        OperationDetails TestPassing( QuestionDto question);
    }
}
