using System;
using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IQuestionService : IDisposable
    {
        IEnumerable<QuestionDto> GetQuestions();
        QuestionDto GetQuestion(int? id);
        void CreateQuestion(QuestionDto questionDto);
        void RemoveQuestion(int id);
        void UpdateQuestion(QuestionDto questionDto);
        void DeleteQuestionFromTest(int? idQuestion, int? idTest);
        IEnumerable<QuestionDto> GetLastQuestions();
    }
}
