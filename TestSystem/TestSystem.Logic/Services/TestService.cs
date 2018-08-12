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
    public class TestService : MapClass<Test, TestDto>, ITestService
    {
        IUnitOfWork Database { get; }

        public TestService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public IEnumerable<ThemeDto> GetAllTheme()
        {
            return MapperFromDB.Map<IEnumerable<Theme>, List<ThemeDto>>(Database.Themes.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TestDto GetTest(int? id)
        {
            if (id == null)
                throw new Exception();
            Test test = Database.Tests.Get(id.Value);
            if (test == null)
                throw new Exception();

            TestDto testDTO = MapperFromDB.Map<TestDto>(test);
            return testDTO;
        }

        public IEnumerable<TestDto> GetTests()
        {
            return MapperFromDB.Map<IEnumerable<Test>, List<TestDto>>(Database.Tests.GetAll());
        }

        public void RemoveTest(int id)
        {
            Test test = Database.Tests.Get(id);
            if (test != null)
            {
                Database.Tests.Remove(test);
                Database.Complete();
            }
        }

        public void CreateTest(TestDto testDTO)
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

            foreach (QuestionDto q in testDTO.Questions)
            {
                test.Questions.Add(Database.Questions.Get(q.IdQuestion));
                test.TotalScore += q.Score;
            }

           
            Database.Tests.Add(test);
            Database.Complete();
        }

        public void UpdateTest(TestDto testDTO)
        {
            Test test = (Test)Database.Tests.Find(x => x.IdTest == testDTO.IdTest);

            if (test != null)
            {
                test = MapperToDB.Map<Test>(testDTO);
                Database.Complete();
            }
        }

        public TestDto GenerateTest(int questionNumbers, int IdTheme, string difficult)
        {
            Random randomGenerate = new Random();

            
            var questionsId = Database.Questions.GetAll().
                Where(x => x.Difficult == difficult && x.IdTheme == IdTheme).
                Select(x => x.IdQuestion).
                OrderBy(x => randomGenerate.Next()).
                Take((int)Math.Round(questionNumbers * 0.8));

            List<int> questions_1 = questionsId.ToList();

            var questionsIdAdding = Database.Questions.GetAll().
                Where(x => x.Difficult != difficult && x.IdTheme != IdTheme).
                Select(x => x.IdQuestion).
                OrderBy(x => randomGenerate.Next()).
                Take(questionNumbers - questions_1.Count());


            List<int> questions_2 = questionsIdAdding.ToList();

            TestDto generateTest = new TestDto
            {
                Difficult = difficult,
                IdTheme = IdTheme
            };
            generateTest.Questions = new List<QuestionDto>();

            foreach (int id in questions_1.Concat(questions_2))
            {
                generateTest.Questions.Add(MapperFromDB.Map<QuestionDto>(Database.Questions.Get(id)));
            }

            return generateTest;
        }

    }

}

