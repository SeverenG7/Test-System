using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.ViewModel;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace TestSystem.Logic.Services
{
    public class TestService : MapClass<Test, TestViewModel>, ITestService
    {
        #region Infrastructure
        IUnitOfWork Database { get; }

        public TestService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        #endregion

        #region Methods

        public void Dispose()
        {
            Database.Dispose();
        }

        public TestViewModel GetTest(int? id)
        {
            if (id == null)
                throw new Exception();
            Test test = Database.Tests.Get(id.Value);
            if (test == null)
                throw new Exception();

            TestViewModel testDTO = MapperFromDB.Map<TestViewModel>(test);
            return testDTO;
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

        public void CreateTest(TestCreateViewModel testDTO , HttpPostedFileBase image)
        {
            Test test = new Test
            {
                CreateDate = DateTime.Now,
                Difficult = testDTO.selectedDifficult,
                IdTheme = Int32.Parse(testDTO.selectedTheme),
                TestDescription = testDTO.TestDescription,
                TestName = testDTO.TestName,
                Time = new TimeSpan(0, testDTO.selectedTime, 0),
            };

            foreach(QuestionForTestViewModel question in testDTO.Questions)
            {
                test.Questions.Add(Database.Questions.Get(question.IdQuestion));
                test.TotalScore += question.Score;
            }

            if (image != null)
            {
                test.ImageMimeType = image.ContentType;
                test.TestImage = new byte[image.ContentLength];
                image.InputStream.Read(test.TestImage, 0, image.ContentLength);
            }

            Database.Tests.Add(test);
            Database.Complete();
        }

        public void UpdateTest(TestViewModel testDTO)
        {
            Test test = (Test)Database.Tests.Find(x => x.IdTest == testDTO.IdTest);

            if (test != null)
            {
                test = MapperToDB.Map<Test>(testDTO);
                Database.Complete();
            }
        }

        public TestViewModel GenerateTest(int questionNumbers, int IdTheme, string difficult)
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

            TestViewModel generateTest = new TestViewModel
            {
                Difficult = difficult,
                IdTheme = IdTheme
            };
            generateTest.Questions = new List<QuestionViewModel>();

            foreach (int id in questions_1.Concat(questions_2))
            {
                generateTest.Questions.Add(MapperFromDB.Map<QuestionViewModel>(Database.Questions.Get(id)));
            }

            return generateTest;
        }

        public IEnumerable<TestViewModel> GetLastTests()
        {
            return MapperFromDB.Map< IEnumerable<Test>, IEnumerable < TestViewModel >>
                (Database.Tests.GetAll().
                 OrderByDescending(x => x.CreateDate).
                 Take(5));
        }

        public TestCreateViewModel GetCreateModel()
        {
            TestCreateViewModel model = new TestCreateViewModel
            {
                Theme = new SelectList(Database.Themes.GetAll(), "IdTheme", "ThemeName")
            };

            IEnumerable<Question> questions = Database.Questions.GetAll();
            model.Questions = new List<QuestionForTestViewModel>();

            foreach (Question question in questions)
            {
                model.Questions.Add(new QuestionForTestViewModel
                {
                    IdQuestion = question.IdQuestion,
                    QuestionText = question.QuestionText,
                    Difficult = question.Difficult,
                    Theme = question.Theme.ThemeName,
                    Chosen = false
                });

            }

            return model;
        }

        #endregion
    }

}

