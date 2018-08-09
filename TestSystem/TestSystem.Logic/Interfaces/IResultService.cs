using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IResultService
    {
        IEnumerable<ResultDto> GetResults();
        ResultDto GetResult(int? id);
        void CreateResult(ResultDto resultDTO);
        void RemoveResult(int id);
        void UpdateResult(ResultDto resultDTO);
        void Dispose();
    }
}
