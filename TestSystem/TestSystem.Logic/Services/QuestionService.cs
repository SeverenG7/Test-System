using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;

namespace TestSystem.Logic.Services
{
    public class QuestionService : MapClass<Question, QuestionDTO>, IQuestionService
    {
        IUnitOfWork Database { get; set; }
        public QuestionService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        public void AddQuestionToTest(int idTest, int idQuestion)
        {
            Database.Tests.Get(idTest).Questions.Add(Database.Questions.Get(idQuestion));
        }
        public void CreateQuestion(QuestionDTO questionDTO)
        {
            Question question = MapperTo.Map<Test>(questionDTO);
            Database.Questions.Add(question);
            Database.Complete();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public QuestionDTO GetQuestion(int? id)
        {
            if (id == null)
                throw new Exception();
            var question = Database.Questions.Get(id.Value);
            if (question == null)
                throw new Exception();
            return MapperFrom.Map<QuestionDTO>(question);
        }

        public IEnumerable<QuestionDTO> GetQuestions()
        {
            return MapperFrom.Map<IEnumerable<Question>, List<QuestionDTO>>(Database.Questions.GetAll());
        }

        public void RemoveQuestion(int id)
        {
            Question question = (Question)Database.Questions.Find(x => x.IdQuestion == id);
            if (question != null)
            {
                Database.Questions.Remove(question);
                Database.Complete();
            }
        }

        public void UpdateQuestion(QuestionDTO questionDTO)
        {
            Question question = (Question)Database.Questions.Find(x => x.IdQuestion == questionDTO.IdQuestion );
            if (question != null)
            {
                question = MapperTo.Map<Test>(question);
                Database.Complete();
            }
        }

        public IEnumerable<AnswerDTO> GetAnswers(QuestionDTO questionDTO)
        {
           return MapperFrom.Map<IEnumerable<Answer>, List<AnswerDTO>>
                (GetQuestion(questionDTO.IdQuestion).Answers);
        }
    }
}
