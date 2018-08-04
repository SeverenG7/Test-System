using System;
using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IQuestionService : IDisposable
    {
        IEnumerable<QuestionDTO> GetQuestions();
        QuestionDTO GetQuestion(int? id);
        void CreateQuestion(QuestionDTO questionDTO);
        void RemoveQuestion(int id);
        void UpdateQuestion(QuestionDTO questionDTO);
        void AddQuestionToTest(int idTest, int idQuestion);


    }
}
