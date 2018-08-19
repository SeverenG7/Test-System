using System;
using System.Collections.Generic;
using System.Web;
using TestSystem.Logic.ViewModel;

namespace TestSystem.Logic.Interfaces
{
    public interface IQuestionService : IDisposable
    {
        QuestionViewModel GetQuestion(int? id);
        QuestionCreateViewModel GetCreationModel(int? id);
        void CreateQuestion(QuestionCreateViewModel questionDto, HttpPostedFileBase image);
        void RemoveQuestion(int id);
        void UpdateQuestion(QuestionCreateViewModel questionDto);
        void DeleteQuestionFromTest(int? idQuestion, int? idTest);
        IEnumerable<QuestionViewModel> GetLastQuestions();
    }
}
