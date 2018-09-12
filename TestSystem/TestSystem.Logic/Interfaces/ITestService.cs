using System.Collections.Generic;
using System.Web;
using TestSystem.Logic.ViewModel;

namespace TestSystem.Logic.Interfaces
{
    public interface ITestService
    {
        TestViewModel GetTest(int? id);
        void CreateTest(TestCreateViewModel testDto, HttpPostedFileBase image);
        void UpdateTest(TestCreateViewModel testDto, HttpPostedFileBase image);
        void RemoveTest(int id);
        TestGenerateViewModel GenerateTest(TestGenerateViewModel model);
        IEnumerable<TestViewModel> GetLastTests();
        TestCreateViewModel GetCreateModel(int? id);
        TestGenerateViewModel GetGenerateViewModel(TestViewModel test);
    }
}
