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
            if (id == null)
                throw new Exception();
            var test = Database.Tests.Get(id.Value);
            if (test == null)
                throw new Exception();

            return new TestDTO { TestName = test.TestName , TestDescription = test.TestDescription , IdTest = test.IdTest};
        }

        public IEnumerable<TestDTO> GetTests()
        {
            var mapper = new MapperConfiguration
                (mapConfig => mapConfig.CreateMap<Test, TestDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Test>, List<TestDTO>>(Database.Tests.GetAll());
        }
    }
}
