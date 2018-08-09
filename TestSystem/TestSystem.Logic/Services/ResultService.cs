using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;

namespace TestSystem.Logic.Services
{
    public class ResultService : MapClass<Result, ResultDto> ,IResultService
    {
        IUnitOfWork Database { get; set; }

        public ResultService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }


        public void CreateResult(ResultDto resultDTO)
        {
            
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ResultDto GetResult(int? id)
        {
            return MapperFromDB.Map<ResultDto>(Database.Results.Get(id.Value));
        }

        public IEnumerable<ResultDto> GetResults()
        {
            return MapperFromDB.Map<IEnumerable<Result>, List<ResultDto>>(Database.Results.GetAll());
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

        public void UpdateResult(ResultDto resultDTO)
        {
            throw new NotImplementedException();
        }
    }
}