using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Infrastructure;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Linq;
using System;
using System.Threading;
using System.Web;
using System.Diagnostics;
using AutoMapper;

namespace TestSystem.Logic.Services
{
    public class TestPassService : MapClass<Question, QuestionDto>, ITestPassService
    {
        static IUnitOfWork Database { get; set; }

        public TestPassService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public QuestionDto StartTest(int IdResult)
        {
            char delimetr = ',';
            TempResult tempResult = new TempResult
            {
                IdResult = IdResult,
                UserName = HttpContext.Current.User.Identity.Name
            };

            foreach (Question question in Database.Results.Get(IdResult).Test.Questions)
            {
                tempResult.QuestionPassing += "" + question.IdQuestion + ",";
            }

            Database.TempResults.Add(tempResult);
            Database.Complete();

            //TimerModule timer = new TimerModule(IdResult, Database.Results.Get(IdResult).Test.Time);
            TimerModule timer = new TimerModule(IdResult, new TimeSpan(0, 5, 0));
            HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name] = timer;
            Question questiondb = Database.Questions.
                Get(Int32.Parse(tempResult.QuestionPassing.Split(delimetr)[0]));
            HttpContext.Current.Application["Test" + HttpContext.Current.User.Identity.Name] = questiondb.IdQuestion;


            var mapper = new MapperConfiguration(mcf => mcf.CreateMap<Question, QuestionDto>()).CreateMapper();
            QuestionDto questionDto = mapper.Map<Question, QuestionDto>(questiondb);
            return questionDto;

        }

        public OperationDetails TestPassing(QuestionDto question)
        {
            TimerModule currentTimer = (TimerModule)HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name];
            TempResult tempResult = Database.TempResults.GetAll().
                Where(x => x.UserName == HttpContext.Current.User.Identity.Name).
                SingleOrDefault();
            PassedQuestion(question, ref tempResult);
            if (currentTimer.StopWatch.IsRunning && !String.IsNullOrWhiteSpace(tempResult.QuestionPassing))
            {
                QuestionDto nextQuestion = MapperFromDB.Map<Question, QuestionDto>
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

            if (tempResult!=null)
            {

                if (String.IsNullOrWhiteSpace(tempResult.QuestionsPassed))
                {
                    QuestionDto question = MapperFromDB.Map<Question, QuestionDto>
                    (Database.Questions.Get(currentId));
                    foreach (AnswerDto answer in question.Answers)
                    {
                        answer.Correct = false;
                    }

                    return new OperationDetails(Math.Round(timer.CurrentInterval().TotalSeconds).ToString(),
                        question);
                }
                if (tempResult.QuestionsPassed.StringStirrer().
                   Contains(currentId))
                {
                    QuestionDto question = MapperFromDB.Map<Question, QuestionDto>
                  (Database.Questions.Get(currentId));
                    foreach (AnswerDto answer in question.Answers)
                    {
                        answer.Correct = false;
                    }

                    return new OperationDetails(Math.Round(timer.CurrentInterval().TotalSeconds).ToString(),
                       question);
                }
                else
                {
                    QuestionDto question = MapperFromDB.Map<Question, QuestionDto>
                (Database.Questions.Get(IdQuestion));
                    foreach (AnswerDto answer in question.Answers)
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
                return new OperationDetails(false , "");
            }

        }

        public OperationDetails Results()
        {
            HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name] = null;
            Result lastResult = Database.Results.GetAll().
                OrderBy(x => x.CreateDate).
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
            return new OperationDetails(true, resultDesrciption , lastResult.Test.TestName , lastResult.ResultScore.ToString(),
                lastResult.UserInfo.UserFirstName);
        }


        private void PassedQuestion(QuestionDto question, ref TempResult tempResult)
        {
            question.IdQuestion = (int)HttpContext.Current.Application["Test" + HttpContext.Current.User.Identity.Name];
            double questionScore = 0;
            Question questionDB = Database.Questions.Get(question.IdQuestion);
            double answerWeight = (questionDB.Score / question.Answers.Count);
            foreach (Answer answer in questionDB.Answers)
            {
                AnswerDto answerUser = question.Answers.Where(x => x.IdAnswer == answer.IdAnswer).SingleOrDefault();
                if (answerUser.Correct == answer.Correct)
                {
                    questionScore += answerWeight;
                }
            }
            tempResult.TotalScore += questionScore;
            tempResult.QuestionPassing = tempResult.QuestionPassing.StringStirrer(question.IdQuestion);
            tempResult.QuestionsPassed += question.IdQuestion.ToString() + ",";
            Database.TempResults.Update(tempResult);


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
                this.testTime = span;
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
                    result.ResultScore = (tempResult.TotalScore * 100) / Database.Results.Get(IdResult).Test.TotalScore;
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
