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

        public void CreateTest(TestCreateViewModel testDTO, HttpPostedFileBase image)
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

            foreach (QuestionForTestViewModel question in testDTO.Questions)
            {
                if (question.Chosen == true)
                {
                    test.Questions.Add(Database.Questions.Get(question.IdQuestion));
                    test.TotalScore += Database.Questions.Get(question.IdQuestion).Score;
                }
            }
            test.QuestionsNumber = test.Questions.Count;
            if (image != null)
            {
                test.ImageMimeType = image.ContentType;
                test.TestImage = new byte[image.ContentLength];
                image.InputStream.Read(test.TestImage, 0, image.ContentLength);
            }

            Database.Tests.Add(test);
            Database.Complete();
        }

        public void UpdateTest(TestCreateViewModel model, HttpPostedFileBase image)
        {
            Test test = Database.Tests.Get(model.IdTest);
            test.CreateDate = DateTime.Now;
            test.Difficult = model.selectedDifficult;
            test.TestDescription = model.TestDescription;
            test.TestName = model.TestName;
            test.Time = new TimeSpan(0, model.selectedTime, 0);
            test.Theme = Database.Themes.Get(Int32.Parse(model.selectedTheme));
            test.TotalScore = 0;

            foreach (QuestionForTestViewModel question in model.Questions)
            {
                if (question.Chosen == true)
                {
                    test.Questions.Add(Database.Questions.Get(question.IdQuestion));
                }
            }

            foreach (Question question in test.Questions)
            {
                test.TotalScore += question.Score;
            }

            test.QuestionsNumber = test.Questions.Count;

            if (image != null)
            {
                test.ImageMimeType = image.ContentType;
                test.TestImage = new byte[image.ContentLength];
                image.InputStream.Read(test.TestImage, 0, image.ContentLength);
            }

            Database.Tests.Update(test);

        }

        public TestGenerateViewModel GenerateTest(TestGenerateViewModel model)
        {
            if (model.Create)
            {
                Test generateTest = new Test
                {
                    Difficult = model.selectedDifficult,
                    IdTheme = Int32.Parse(model.selectedTheme),
                    TestDescription = model.TestDescription,
                    TestName = model.TestName,
                    CreateDate = DateTime.Now,
                    Time = new TimeSpan(0,model.selectedTime,0),
                    QuestionsNumber = model.selectedNumber,
                };

                generateTest.Questions = new List<Question>();

                foreach (QuestionViewModel question in model.Questions)
                {
                    generateTest.Questions.Add((Database.Questions.Get(question.IdQuestion)));
                    generateTest.TotalScore += question.Score;
                }

                Database.Tests.Add(generateTest);
                Database.Complete();
                return null;
            }

            else
            {
                model.Questions.Clear();

                Random randomGenerate = new Random();

                var questionsId = Database.Questions.GetAll().
                    Where(x => x.Difficult == model.selectedDifficult
                    && x.IdTheme == Int32.Parse(model.selectedTheme)).
                    Select(x => x.IdQuestion).
                    OrderBy(x => randomGenerate.Next()).
                    Take((int)Math.Round(model.selectedNumber * 0.8));

                List<int> questions_1 = questionsId.ToList();

                var questionsIdAdding = Database.Questions.GetAll().
                    Where(x => x.Difficult != model.selectedDifficult
                    || x.IdTheme != Int32.Parse(model.selectedTheme)).
                    Select(x => x.IdQuestion).
                    OrderBy(x => randomGenerate.Next()).
                    Take(model.selectedNumber - questions_1.Count());


                List<int> questions_2 = questionsIdAdding.ToList();


                foreach (int id in questions_1.Concat(questions_2))
                {
                    model.Questions.Add(MapperFromDB.Map<QuestionViewModel>(Database.Questions.Get(id)));
                }

                model.Theme = new SelectList(Database.Themes.GetAll(), "IdTheme", "ThemeName");

                return model;
            }

        }

        public TestGenerateViewModel GetGenerateViewModel(TestViewModel test)
        {
            TestGenerateViewModel model = new TestGenerateViewModel
            {
                Theme = new SelectList(Database.Themes.GetAll(), "IdTheme", "ThemeName")
            };
            return model;

        }

        public IEnumerable<TestViewModel> GetLastTests()
        {
            List<Test> tests = Database.Tests.GetAll().
                 OrderByDescending(x => x.CreateDate).
                 Take(5).ToList();

            List<TestViewModel> lastTests = new List<TestViewModel>();

            foreach (Test test in tests)
            {

                lastTests.Add(new TestViewModel
                {
                    IdTest = test.IdTest,
                    TestName = test.TestName,
                    CreateDate = test.CreateDate
                });
            }
            return lastTests;
        }

        public TestCreateViewModel GetCreateModel(int? id)
        {
            if (!id.HasValue)
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
            else
            {
                Test test = Database.Tests.Get(id.Value);
                if (test != null)
                {
                    TestCreateViewModel model = new TestCreateViewModel
                    {
                        Theme = new SelectList(Database.Themes.GetAll(), "IdTheme", "ThemeName"),
                        selectedDifficult = test.Difficult,
                        TestDescription = test.TestDescription,
                        selectedTime = test.Time.Minutes,
                        IdTest = test.IdTest,
                        TestName = test.TestName,
                        ImageMimeType = test.ImageMimeType,
                        TestImage = test.TestImage
                    };

                    if (test.Theme == null)
                    {
                        model.selectedTheme = "";
                    }
                    else
                    {
                        model.selectedTheme = test.Theme.ThemeName;
                    }

                    model.Questions = new List<QuestionForTestViewModel>();

                    foreach (Question question in Database.Questions.GetAll())
                    {
                        if (!test.Questions.Contains(question))
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

                    }

                    return model;
                }
                else
                {
                    return null;
                }

            }
        }

        #endregion
    }

}

