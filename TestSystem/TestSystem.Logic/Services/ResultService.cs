using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Linq;

namespace TestSystem.Logic.Services
{
    public class ResultService : MapClass<Result, ResultDto>, IResultService
    {
        IUnitOfWork Database { get; }

        public ResultService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }


        public void GivePremission(int IdTest, string IdUser , string Description)
        {
            Result result = new Result
            {
                CreateDate = DateTime.Now,
                IdTest = IdTest,
                IdUserInfo = IdUser,
                ResultDescription = Description,
                ResultScore = null,
            };
            Database.Results.Add(result);
            Database.Complete();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ResultDto GetResult(int? id)
        {
            return MapperFromDB.Map<ResultDto>(Database.Results.Get(id.Value));
        }

        public IEnumerable<ResultDto> GetResultsById(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return MapperFromDB.Map<IEnumerable<Result>, List<ResultDto>>(Database.Results.GetAll());
            }
            else
            {
                return MapperFromDB.Map<IEnumerable<Result>, List<ResultDto>>(Database.Results.GetAll().
                    Where(x => x.IdUserInfo == id));
            }
        }

        public IEnumerable<ResultDto> GetResults()
        {

            return MapperFromDB.Map<IEnumerable<Result>, List<ResultDto>>(Database.Results.GetAll());


        }

        public void RemoveResult(int id)
        {
        }

        public void UpdateResult(ResultDto resultDTO)
        {
            throw new NotImplementedException();
        }

        public UserInfoDto GetUser(int id)
        {
            return MapperFromDB.Map<UserInfoDto>(Database.Results.Get(id));
        }

        public IEnumerable<UserInfoDto> GetUsers(string search)
        {
            if (String.IsNullOrEmpty(search))
            {
                return MapperFromDB.Map<IEnumerable<UserInfo>, List<UserInfoDto>>(Database.UserInfoes.GetAll());
            }
            else
            {
                var users = Database.UserInfoes.GetAll().
                     Where(x => x.UserFirstName.Contains(search) ||
                     x.UserLastName.Contains(search) ||
                     x.ApplicationUser.Email.Contains(search));
                return MapperFromDB.Map<IEnumerable<UserInfo>, List<UserInfoDto>>(users.AsEnumerable());
            }
        }
    }
}