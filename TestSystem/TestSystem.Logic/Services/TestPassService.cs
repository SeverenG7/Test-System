using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Collections.Generic;
using System;

namespace TestSystem.Logic.Services
{
    public class TestPassService : MapClass<Question, QuestionDto>, ITestPassService
    {
        IUnitOfWork Database { get; }

        public TestPassService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public QuestionDto StartTest(int IdResult)
        {
            char delimetr = ',';
            TempResult tempResult = new TempResult
            {
                IdResult = IdResult
            };

            foreach (Question question in Database.Results.Get(IdResult).Test.Questions)
            {
                tempResult.QuestionPassing += "" + question.IdQuestion + ",";
            }
            Int32.Parse(tempResult.QuestionPassing.Split(delimetr)[0]);
            return MapperFromDB.Map<Question, QuestionDto>(Database.Questions.
                Get(Int32.Parse(tempResult.QuestionPassing.Split(delimetr)[0])));
        }
    }
}
