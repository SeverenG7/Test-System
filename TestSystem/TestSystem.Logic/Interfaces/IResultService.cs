using System.Collections.Generic;
using TestSystem.Logic.ViewModel;

namespace TestSystem.Logic.Interfaces
{
    public interface IResultService
    {
        ResultFullViewModel GetResult(int? id);
        void GivePremission(PremissionViewModel model);
        List<ResultFullViewModel> GetLastResults();
        ResultInfoViewModel GetResultInfo(int IdResult);
        ResultViewModel GetAllResults(string search, string id);
        PremissionViewModel CreatePremissionModel(string IdUser);
        void Dispose();
    }
}
