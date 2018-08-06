using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;


namespace TestSystem.Logic.Services
{
    public class TestService : MapClass<Test, TestDTO>, ITestService
    {
        IUnitOfWork Database { get; set; }
        IUnitOfWorkUpdate Update { get; set; }

        public TestService(IUnitOfWork unitOfWork, IUnitOfWorkUpdate unitOfWorkUpdate)
        {
            Database = unitOfWork;
            Update = unitOfWorkUpdate;
        }

        public IEnumerable<ThemeDTO> GetAllTheme()
        {
            return MapperFromDB.Map<IEnumerable<Theme>, List<ThemeDTO>>(Database.Themes.GetAll());
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
            if (test != null)
            {
                Database.Tests.Remove(test);
                Database.Complete();
            }
        }

        public void CreateTest(TestDTO testDTO)
        {
            Test test = new Test
            {
                CreateDate = testDTO.CreateDate,
                Difficult = testDTO.Difficult,
                IdTheme = testDTO.IdTheme,
                TestDescription = testDTO.TestDescription,
                TestName = testDTO.TestName,
                QuestionsNumber = testDTO.QuestionsNumber
            };

            foreach (QuestionDTO q in testDTO.Questions)
            {
                test.Questions.Add(Database.Questions.Get(q.IdQuestion));
            }

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
