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

namespace TestSystem.Logic.Services
{
    public class TestPassService : MapClass<Question, QuestionDto>, ITestPassService
    {
        static IUnitOfWork Database { get; set; }
        public TimerModule Timer { get; set; }

        public TestPassService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public QuestionDto StartTest(int IdResult)
        {
            if (Database.TempResults.Get(IdResult) == null)
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

                TimerModule timer = new TimerModule(IdResult);
                timer.StartTimer();
                timer.StopWatch.Start();
                HttpContext.Current.Application["Timer" + IdResult.ToString()] = timer;

                Int32.Parse(tempResult.QuestionPassing.Split(delimetr)[0]);
                return MapperFromDB.Map<Question, QuestionDto>(Database.Questions.
                    Get(Int32.Parse(tempResult.QuestionPassing.Split(delimetr)[0])));
            }
            else
            {
                return MapperFromDB.Map<Question, QuestionDto>(Database.Questions.
                    GetAll().FirstOrDefault());
            }
        }


        public OperationDetails UserPassingTest(string userName)
        {
            TempResult tempResult = Database.TempResults.GetAll().
                Where(x => x.UserName == userName).FirstOrDefault();
            if (tempResult != null)
            {
                return new OperationDetails(true, tempResult.IdResult);
            }

            else
            {
                return new OperationDetails(false, "");
            }
        }

        public QuestionDto TestPassing(int IdResult)
        {
            Database.TempResults.Get(IdResult);
            return MapperFromDB.Map<Question,QuestionDto>( Database.Questions.GetAll().FirstOrDefault());
        }


        #region Timer custom class

        public class TimerModule
        {
            public Stopwatch StopWatch { get; set; }
            public Timer Timer { get; set; }
            int IdResult { get; set; }
            long interval = 60000;
            static object synclock = new object();
            static bool testEnd = false;
            TimeSpan testTime;

            public TimerModule(int IdResult)
            {
                this.IdResult = IdResult;
            }

            public void StartTimer()
            {
                Timer = new Timer(new TimerCallback(EndTimer), null, interval, 0);
                StopWatch = new Stopwatch();
                testTime = new TimeSpan(0, 1, 0);
            }

            public TimeSpan CurrentInterval()
            {
                return (testTime.Subtract(StopWatch.Elapsed));
            }

            public void EndTimer(object obj)
            {
                if (testEnd == false)
                {
                    Result result = Database.Results.Get(IdResult);
                    TempResult tempResult = Database.TempResults.Get(IdResult);
                    result.ResultScore = tempResult.TotalScore;
                    result.TestPassed = true;
                    Database.TempResults.Remove(tempResult);
                    Database.Results.Update(result);
                    Database.Complete();
                }

            }
        }

        #endregion
    }
}
