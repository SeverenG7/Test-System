using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Linq.Expressions;

namespace TestSystem.Logic.Services
{
    public class ResultService : MapClass<Result, ResultDTO> ,IResultService
    {
        IUnitOfWork Database { get; set; }

        public ResultService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }


        public void CreateResult(ResultDTO resultDTO)
        {
            
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ResultDTO GetResult(int? id)
        {
            Result result = Database.Results.Get(id.Value);
            ResultDTO resultDTO = MapperFromDB.Map<ResultDTO>(result);
            return resultDTO;
        }

        public IEnumerable<ResultDTO> GetResults()
        {
            return MapperFromDB.Map<IEnumerable<Result>, List<ResultDTO>>(Database.Results.GetAll());
        }

        public void RemoveResult(int id)
        {
            Result result = (Result)Database.Results.Find(x => x.IdResult == id);
            if (result != null)
            {
                Database.Results.Remove(result);
                Database.Complete();
            }
        }

        public void UpdateResult(ResultDTO resultDTO)
        {
            throw new NotImplementedException();
        }
    }
}