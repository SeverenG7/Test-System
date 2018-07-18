using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Threading.Tasks;
using AutoMapper;

namespace TestSystem.Logic.Services
{
    public class TestService : MapClass<Test,TestDTO> ,ITestService
    {
        IUnitOfWork Database { get; set; }

        public TestService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TestDTO GetTest(int? id)
        {
            if (id == null)
                throw new Exception();
            Test test = Database.Tests.Get(id.Value);
            if (test == null)
                throw new Exception();

            TestDTO testDTO = MapperFromDB.Map<TestDTO>(test);
            return testDTO;
        }

        public IEnumerable<TestDTO> GetTests()
        {
            return MapperFromDB.Map<IEnumerable<Test>, List<TestDTO>>(Database.Tests.GetAll());
        }

        public void RemoveTest(int id)
        {
            Test test = (Test)Database.Tests.Find(x => x.IdTest == id);
            if(test != null)
            {
                Database.Tests.Remove(test);
                Database.Complete();
            }
        }

        public void CreateTest(TestDTO testDTO)
        {
            Test test = MapperToDB.Map<Test>(testDTO);
            Database.Tests.Add(test);
            Database.Complete();
        }

        public void UpdateTest(TestDTO testDTO)
        {
            Test test = (Test)Database.Tests.Find(x => x.IdTest == testDTO.IdTest);

            if (test != null)
            {
                test = MapperToDB.Map<Test>(testDTO);
                Database.Complete();
            }
        }

    }
}
