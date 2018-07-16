using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestDTO> GetTests();
        TestDTO GetTest(int? id);
        void CreateTest(TestDTO testDTO);
        void RemoveTest(int id);
        void UpdateTest(TestDTO testDTO);
        void Dispose();
    }
}
