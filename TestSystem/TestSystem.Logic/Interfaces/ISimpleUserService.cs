using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface ISimpleUserService
    {
        UserDTO GetUser(int? id);
        void RemoveResult(int id);
        void UpdateResult(ResultDTO resultDTO);
        void Dispose();
    }

}

