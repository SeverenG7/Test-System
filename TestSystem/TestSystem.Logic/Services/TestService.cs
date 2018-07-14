using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.DataProvider.Repositories;
using TestSystem.DataProvider.BaseClasses;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using AutoMapper;


namespace TestSystem.Logic.Services
{
    public class TestService : ITestService
    {
        IUnitOfWork Database { get; set; }

        public TestService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TestDTO GetTest(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TestDTO> GetTests()
        {
            var mapper = new MapperConfiguration
                (mapConfig => mapConfig.CreateMap<Test, TestDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Test>, List<TestDTO>>(Database.Tests.GetAll());
        }


    }
}
