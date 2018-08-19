using System.Collections.Generic;
using System.Web;
using TestSystem.Logic.ViewModel;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestService
    {
        TestViewModel GetTest(int? id);
        void CreateTest(TestCreateViewModel testDto, HttpPostedFileBase image);
        void RemoveTest(int id);
        TestViewModel GenerateTest(int questionNumbers , int idTheme , string difficult);
        IEnumerable<TestViewModel> GetLastTests();
        TestCreateViewModel GetCreateModel();

    }
}
