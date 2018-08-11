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
using System.Collections.Generic;
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
            TimerModule timer = new TimerModule(IdResult , new TimeSpan(0,5,0));
            HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name] = timer;





            //Question questionW = Database.Questions.
            //        Get(Int32.Parse(tempResult.QuestionPassing.Split(delimetr)[0]));




            //QuestionDto questionDto = new QuestionDto()
            //{
            //    IdQuestion = questionW.IdQuestion,
            //    QuestionText = questionW.QuestionText
            //};

            //questionDto.Answers = new List<AnswerDto>();

            //foreach (Answer answer in questionW.Answers)
            //{
            //    questionDto.Answers.Add(new AnswerDto
            //    {
            //        AnswerText = answer.AnswerText,
            //        IdAnswer = answer.IdAnswer,
            //        Correct = answer.Correct
            //    });
            //}

            //return questionDto;

            Question questiondb = Database.Questions.
                Get(Int32.Parse(tempResult.QuestionPassing.Split(delimetr)[0]));


            var mapper = new MapperConfiguration(mcf => mcf.CreateMap<Question, QuestionDto>()).CreateMapper();
            QuestionDto questionDto = mapper.Map<Question, QuestionDto>(questiondb);
            return questionDto;

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

        public QuestionDto TestPassing( QuestionDto question)
        {
            TimerModule currentTimer = (TimerModule)HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name];
            TempResult tempResult = Database.TempResults.GetAll().
                Where(x => x.UserName == HttpContext.Current.User.Identity.Name).
                SingleOrDefault();
            PassedQuestion(question, ref tempResult);
            if (currentTimer.StopWatch.IsRunning && !String.IsNullOrWhiteSpace(tempResult.QuestionPassing))
            {
                return MapperFromDB.Map<Question, QuestionDto>
                    (Database.Questions.Get(tempResult.QuestionPassing.StringStirrer().FirstOrDefault()));
            }

            else
            {
                EndTestPassing(tempResult);
                return null;
            }
        }

        private void PassedQuestion(QuestionDto question, ref TempResult tempResult)
        {
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
            Database.TempResults.Update(tempResult);
        }

        private void EndTestPassing(TempResult tempResult)
        {
            TimerModule currentTimer = (TimerModule)HttpContext.Current.Application["Timer" + HttpContext.Current.User.Identity.Name];
            object obj = new  object();
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

            public TimerModule(int IdResult , TimeSpan span)
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
                    result.ResultScore = tempResult.TotalScore;
                    result.TestPassed = true;
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
