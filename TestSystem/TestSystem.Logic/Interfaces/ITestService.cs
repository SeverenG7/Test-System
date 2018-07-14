using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestDTO> GetTests();
        TestDTO GetTest(int? id);
        void Dispose();
    }
}
