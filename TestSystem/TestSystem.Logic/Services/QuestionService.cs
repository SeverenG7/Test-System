using System;
using System.Collections.Generic;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using AutoMapper;

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
            Question question = MapperToDB.Map<Question>(questionDTO);
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
            Question question = Database.Questions.Get(id.Value);
            if (question == null)
                throw new Exception();
            var mapper = new MapperConfiguration(mcf => mcf.CreateMap<Question, QuestionDTO>()).CreateMapper();
            return mapper.Map<Question,QuestionDTO>(question);
        }

        public IEnumerable<QuestionDTO> GetQuestions()
        {
            return MapperFromDB.Map<IEnumerable<Question>, List<QuestionDTO>>(Database.Questions.GetAll());
        }

        public void RemoveQuestion(int id)
        {
            Question question = Database.Questions.Get(id);
            if (question != null)
            {
                Database.Questions.Remove(question);
                Database.Complete();
            }
        }

        public void UpdateQuestion(QuestionDTO questionDTO)
        {
            //truly magic i guess
            foreach (AnswerDTO ans in questionDTO.Answers)
            {
                Answer answer = Database.Answers.Get(ans.IdAnswer);
                Database.Answers.Updating(answer);
            }
            Question question = Database.Questions.Get(questionDTO.IdQuestion);
            Database.Questions.Updating(question);
            Question questionUpdate = MapperToDB.Map<Question>(questionDTO);
            foreach (Answer ans in questionUpdate.Answers)
            {
                Database.Answers.Update(ans);
            }
            Database.Questions.Update(questionUpdate);
        }
    }
}
