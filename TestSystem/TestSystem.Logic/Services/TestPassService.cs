using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Infrastructure;
using TestSystem.Logic.ViewModel;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Linq;
using System;
using System.Threading;
using System.Web;
using System.Diagnostics;
using AutoMapper;
using System.Collections.Generic;

namespace TestSystem.Logic.Services
{
    public class TestPassService : MapClass<Question, QuestionViewModel>, ITestPassService
    {
        static IUnitOfWork Database { get; set; }

        public TestPassService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public QuestionViewModel StartTest(int IdResult)
        {
            char delimetr = ',';
            List<UserQuestion> userQuestions = new List<UserQuestion>();
            TempResult tempResult = new TempResult
            {
                IdResult = IdResult,
                UserName = HttpContext.Current.User.Identity.Name,
            };

            foreach (Question question in Database.Results.Get(IdResult).Test.Questions)
            {
                tempResult.QuestionPassing += "" + question.IdQuestion + ",";
                userQuestions.Add(new UserQuestion()
                {
                    IdResult = IdResult,
                    IdQuestion = question.IdQuestion,
                    MaxScore = question.Score,
                    UserAnswers = new List<UserAnswer>()
                });
            }
            Database.UserQuestions.AddRange(userQuestions);
            Database.TempResults.Add(tempResult);
            Database.Complete();

            TimerModule timer = new TimerModule(IdResult, Database.Results.Get(IdResult).Test.Time);
            HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name] = timer;
            Question questiondb = Database.Questions.
                Get(Int32.Parse(tempResult.QuestionPassing.Split(delimetr)[0]));
            HttpContext.Current.Application["Test" + HttpContext.Current.User.Identity.Name] = questiondb.IdQuestion;

            var mapper = new MapperConfiguration(mcf => mcf.CreateMap<Question, QuestionViewModel>()).CreateMapper();
            QuestionViewModel questionDto = mapper.Map<Question, QuestionViewModel>(questiondb);
            return questionDto;

        }

        public OperationDetails TestPassing(QuestionViewModel question)
        {
            TimerModule currentTimer = (TimerModule)HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name];

            TempResult tempResult = Database.TempResults.GetAll().
                Where(x => x.UserName == HttpContext.Current.User.Identity.Name).
                SingleOrDefault();
            PassedQuestion(question, ref tempResult);
            if (currentTimer.StopWatch.IsRunning && !String.IsNullOrWhiteSpace(tempResult.QuestionPassing))
            {
                QuestionViewModel nextQuestion = MapperFromDB.Map<Question, QuestionViewModel>
                    (Database.Questions.Get(tempResult.QuestionPassing.StringStirrer().FirstOrDefault()));
                HttpContext.Current.Application["Test" + HttpContext.Current.User.Identity.Name] = nextQuestion.IdQuestion;
                return new OperationDetails(true, nextQuestion);
            }

            else
            {
                EndTestPassing(tempResult);
                return new OperationDetails(false, null);
            }

        }

        public OperationDetails GetCurrentTestState(int IdQuestion)
        {
            TempResult tempResult = Database.TempResults.
                 Find(x => x.UserName == HttpContext.Current.User.Identity.Name).
                 SingleOrDefault();

            int currentId = Int32.Parse(
                HttpContext.Current.Application["Test" + HttpContext.Current.User.Identity.Name].ToString());

            TimerModule timer = (TimerModule)
              HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name];

            if (tempResult != null)
            {
                if (String.IsNullOrWhiteSpace(tempResult.QuestionsPassed))
                {
                    QuestionViewModel question = MapperFromDB.Map<Question, QuestionViewModel>
                    (Database.Questions.Get(currentId));
                    foreach (AnswerViewModel answer in question.Answers)
                    {
                        answer.Correct = false;
                    }

                    return new OperationDetails(Math.Round(timer.CurrentInterval().TotalSeconds).ToString(),
                        question);
                }
                if (tempResult.QuestionPassing.StringStirrer().
                   Contains(currentId))
                {
                    QuestionViewModel question = MapperFromDB.Map<Question, QuestionViewModel>
                  (Database.Questions.Get(currentId));
                    foreach (AnswerViewModel answer in question.Answers)
                    {
                        answer.Correct = false;
                    }

                    return new OperationDetails(Math.Round(timer.CurrentInterval().TotalSeconds).ToString(),
                       question);
                }
                else
                {
                    QuestionViewModel question = MapperFromDB.Map<Question, QuestionViewModel>
                (Database.Questions.Get(IdQuestion));
                    foreach (AnswerViewModel answer in question.Answers)
                    {
                        answer.Correct = false;
                    }

                    return new OperationDetails(
                        Math.Round(timer.CurrentInterval().TotalSeconds).ToString(),
                            question);
                }
            }
            else
            {
                return new OperationDetails(false, "");
            }

        }

        public OperationDetails Results()
        {
            HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name] = null;
            Result lastResult = Database.Results.GetAll().
                OrderByDescending(x => x.CreateDate).
                FirstOrDefault();

            string resultDesrciption;

            if (lastResult.ResultScore < 60)
            {
                resultDesrciption = "Sorry , but you didn't get a norm " +
                    "to pass this test :( Actually, you always can study some more and try again!";
            }
            if (lastResult.ResultScore > 60 && lastResult.ResultScore < 80)
            {
                resultDesrciption = "Congratulation, you score actually good result!" +
                    "Test was passed, but here were a lot of moments where you could try better!";
            }
            else
            {
                resultDesrciption = "Wow,you really easily passed this test, congratulations! ";
            }
            lastResult.ResultScore = Math.Round(lastResult.ResultScore.Value);
            return new OperationDetails(true, resultDesrciption, lastResult.Test.TestName, lastResult.ResultScore.ToString(),
                lastResult.UserInfo.UserFirstName);
        }

        private void PassedQuestion(QuestionViewModel question, ref TempResult tempResult)
        {
            int IdResult = tempResult.IdResult;
            question.IdQuestion = (int)HttpContext.Current.Application["Test" + HttpContext.Current.User.Identity.Name];
            UserQuestion userQuestion = Database.UserQuestions.Find(x => x.IdResult == IdResult
              && x.IdQuestion == question.IdQuestion).
              SingleOrDefault();
            double questionScore = 0;
            Question questionDB = Database.Questions.Get(question.IdQuestion);
            double answerWeight = (questionDB.Score / question.Answers.Count);
            foreach (Answer answer in questionDB.Answers)
            {
                AnswerViewModel answerUser = question.Answers.Where(x => x.IdAnswer == answer.IdAnswer).SingleOrDefault();
                userQuestion.UserAnswers.Add(new UserAnswer()
                {
                    IdUserQuestion = userQuestion.IdUserQuestion,
                    IdAnswer = answer.IdAnswer,
                    Correct = answerUser.Correct
                });
                if (answerUser.Correct == answer.Correct)
                {
                    questionScore += answerWeight;
                    userQuestion.UserScore += answerWeight;
                }
            }
            tempResult.TotalScore += questionScore;
            tempResult.QuestionPassing = tempResult.QuestionPassing.StringStirrer(question.IdQuestion);
            tempResult.QuestionsPassed += question.IdQuestion.ToString() + ",";
            Database.UserAnswers.AddRange(userQuestion.UserAnswers);
            Database.Complete();
            Database.UserQuestions.Update(userQuestion);
            Database.TempResults.Update(tempResult);
            Database.Complete();
        }

        private void EndTestPassing(TempResult tempResult)
        {
            TimerModule currentTimer = (TimerModule)HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name];
            HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name] = null;
            object obj = new object();
            currentTimer.EndTimer(obj);
        }

        #region Timer custom class

        public class TimerModule
        {
            private static object _synclock = new object();
            private static bool _testEnd = false;
            private readonly Timer _timer;

            public Stopwatch StopWatch = new Stopwatch();
            public int IdResult { get; set; }
            public TimeSpan testTime;

            public TimerModule(int IdResult, TimeSpan span)
            {
                this.IdResult = IdResult;
                testTime = span;
                _timer = new Timer(new TimerCallback(EndTimer), null, testTime.Minutes * 60000, 0);
                StopWatch.Start();
            }

            public TimeSpan CurrentInterval()
            {
                return (testTime.Subtract(StopWatch.Elapsed));
            }

            public void StopTimer()
            {
                _timer.Dispose();
                StopWatch.Stop();
            }

            public void EndTimer(object obj)
            {
                if (_testEnd == false)
                {
                    Result result = Database.Results.Get(IdResult);
                    TempResult tempResult = Database.TempResults.Get(IdResult);
                    result.ResultScore = ((tempResult.TotalScore * 100) / Database.Results.Get(IdResult).Test.TotalScore);
                    result.TestPassed = true;
                    result.CreateDate = DateTime.Now;
                    Database.TempResults.Remove(tempResult);
                    Database.Results.Update(result);
                    Database.Complete();
                    StopTimer();
                }
            }
        }

        #endregion
    }
}
