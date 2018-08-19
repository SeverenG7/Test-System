using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Model.Models;
using TestSystem.Logic.ViewModel;
using TestSystem.Logic.MapGeneric;
using System.Linq;

namespace TestSystem.Logic.Services
{
    public class ResultService : MapClass<Result, ResultFullViewModel>, IResultService
    {
        #region Infrastructure
        IUnitOfWork Database { get; }
        public ResultService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        #endregion

        #region Methods

        public void GivePremission(PremissionViewModel model)
        {
            int IdTest = model.Tests.
                Where(x => x.Choosen == true).
                FirstOrDefault().IdTest;

            Result result = new Result
            {
                CreateDate = DateTime.Now,
                IdTest = IdTest,
                IdUserInfo = model.UserResult.IdUserInfo,
                ResultDescription = model.UserResult.ResultDescription,
                ResultScore = null,
            };
            Database.Results.Add(result);
            Database.Complete();
        }

        public ResultFullViewModel GetResult(int? id)
        {
            return MapperFromDB.Map<Result, ResultFullViewModel>(Database.Results.Get(id.Value));
        }

        public List<ResultFullViewModel> GetLastResults()
        {
            return MapperFromDB.Map<IEnumerable<Result>, List<ResultFullViewModel>>
                (Database.Results.GetAll().
                OrderByDescending(x => x.CreateDate).
                Take(5));
        }

        public ResultInfoViewModel GetResultInfo(int IdResult)
        {
            List<QuestionResultViewModel> resultQuestions = new List<QuestionResultViewModel>();
            Result result = Database.Results.Get(IdResult);
            if (result.TestPassed != false)
            {
                foreach (Question question in result.Test.Questions)
                {
                    UserQuestion userQuestion = Database.UserQuestions.Find(x => x.IdQuestion == question.IdQuestion &&
                    x.IdResult == IdResult).
                        SingleOrDefault();
                    resultQuestions.Add(new QuestionResultViewModel(question, userQuestion));
                    foreach (Answer answer in question.Answers)
                    {
                        resultQuestions.LastOrDefault().Answers.Add(new AnswerResultViewModel(answer, userQuestion.UserAnswers.
                            Where(x => x.IdAnswer == answer.IdAnswer).
                            SingleOrDefault()));
                    }
                }
                return new ResultInfoViewModel(result, resultQuestions);
            }
            else
            {
                return null;
            }
        }

        public ResultViewModel GetAllResults(string search, string id)
        {
            ResultViewModel model = new ResultViewModel();
            if (String.IsNullOrEmpty(search))
            {
                model.Users = Database.UserInfoes.GetAll().ToList();
            }
            else
            {
                var users = Database.UserInfoes.GetAll().
                     Where(x => x.UserFirstName.Contains(search) ||
                     x.UserLastName.Contains(search) ||
                     x.ApplicationUser.Email.Contains(search));
                model.Users = users.ToList();
            }


            if (String.IsNullOrEmpty(id))
            {
                model.Results = Database.Results.GetAll().ToList();
            }
            else
            {
                model.Results = Database.Results.GetAll().
                    Where(x => x.IdUserInfo == id).ToList();
            }

            return model;
        }

        public PremissionViewModel CreatePremissionModel(string IdUser, string sortOrder)
        {
            PremissionViewModel model = new PremissionViewModel();
            model.UserResult.IdUserInfo = IdUser;

            foreach (Test test in Database.Tests.GetAll())
            {
                model.Tests.Add(new TestPremissionViewModel
                {
                    TestName = test.TestName,
                    Difficult = test.Difficult,
                    IdTest = test.IdTest,
                    TestDescription = test.TestDescription,
                    Theme = test.Theme
                });
            }

            if (sortOrder != null)
            {
                switch (sortOrder)
                {
                    case "difficult_desc":
                       model.Tests  = model.Tests.OrderByDescending(x=> x.Difficult).ToList();
                        break;
                    case "Difficult":
                        model.Tests = model.Tests.OrderBy(x => x.Difficult).ToList();
                        break;
                    case "Name":
                        model.Tests = model.Tests.OrderBy(x => x.TestName).ToList();
                        break;
                    case "name_desc":
                        model.Tests = model.Tests.OrderByDescending(x => x.TestName).ToList();
                        break;
                }
            }


            return model;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        #endregion
    }
}






