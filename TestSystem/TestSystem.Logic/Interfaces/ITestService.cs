using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestDto> GetTests();
        TestDto GetTest(int? id);
        void CreateTest(TestDto testDto);
        void RemoveTest(int id);
        TestDto GenerateTest(int questionNumbers , int idTheme , string difficult);

    }
}
