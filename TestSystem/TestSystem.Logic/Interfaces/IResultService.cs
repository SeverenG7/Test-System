using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IResultService
    {
        IEnumerable<ResultDTO> GetResults();
        ResultDTO GetResult(int? id);
        void CreateResult(ResultDTO resultDTO);
        void RemoveResult(int id);
        void UpdateResult(ResultDTO resultDTO);
        void Dispose();
    }
}
