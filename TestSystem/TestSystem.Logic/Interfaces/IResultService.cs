using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IResultService
    {
        IEnumerable<ResultDto> GetResultsById(string id);
        IEnumerable<ResultDto> GetResults();
        ResultDto GetResult(int? id);
        void GivePremission(int IdTest , string IdUser , string Description);
        void RemoveResult(int id);
        void UpdateResult(ResultDto resultDTO);
        UserInfoDto GetUser(int id);
        IEnumerable<UserInfoDto> GetUsers(string search);
        IEnumerable<ResultDto> GetLastResults();
        void Dispose();
    }
}
