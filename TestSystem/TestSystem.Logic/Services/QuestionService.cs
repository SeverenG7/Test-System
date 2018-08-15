using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using AutoMapper;

namespace TestSystem.Logic.Services
{
    public class QuestionService : MapClass<Question, QuestionDto>, IQuestionService
    {
        private IUnitOfWork Database { get;}
        public QuestionService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        public void CreateQuestion(QuestionDto questionDTO)
        {
            questionDTO.Score = ComputeScore(questionDTO);
            Question question = MapperToDB.Map<Question>(questionDTO);
            Database.Questions.Add(question);
            Database.Answers.AddRange(question.Answers);
            Database.Complete();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public QuestionDto GetQuestion(int? id)
        {
            if (id == null)
                throw new Exception();
            Question question = Database.Questions.Get(id.Value);
            if (question == null)
                throw new Exception();
            var mapper = new MapperConfiguration(mcf => mcf.CreateMap<Question, QuestionDto>()).CreateMapper();
            return mapper.Map<Question,QuestionDto>(question);
        }

        public IEnumerable<QuestionDto> GetQuestions()
        {
            return MapperFromDB.Map<IEnumerable<Question>, List<QuestionDto>>(Database.Questions.GetAll());
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

        public void UpdateQuestion(QuestionDto questionDTO)
        {
            foreach (AnswerDto ans in questionDTO.Answers)
            {
                Answer answer = Database.Answers.Get(ans.IdAnswer);
                answer.AnswerText = ans.AnswerText;
                answer.Correct = ans.Correct;
                Database.Answers.Update(answer);
            }
            Database.Questions.Update(Database.Questions.Get(questionDTO.IdQuestion));
            Database.Complete();
        }

        public void DeleteQuestionFromTest(int? IdQuestion, int? IdTest)
        {
            if (IdQuestion.HasValue && IdTest.HasValue)
            {
                Question question = Database.Questions.Get(IdQuestion.Value);
                Test test = Database.Tests.Get(IdTest.Value);
                if (question.Tests.Contains(test))
                {
                    question.Tests.Remove(test);
                }
                test.Questions.Remove(question);
                Database.Tests.Update(test);
                Database.Questions.Update(question);
                Database.Complete();
            }
        }

        public IEnumerable<QuestionDto> GetLastQuestions()
        {
            return MapperFromDB.Map<IEnumerable<Question>, IEnumerable<QuestionDto>>
                (Database.Questions.GetAll().
                OrderBy(x => x.CreateDate).
                Take(5));
        }

        public  int ComputeScore(QuestionDto question)
        {
            int koeff = 0;
            switch (question.Difficult)
            {
                case "Junior":
                    koeff = 1;
                    break;
                case "Middle":
                    koeff = 2;
                    break;
                case "Senior":
                    koeff = 3;
                    break;
            }
            return (koeff * question.AnswerNumber);
        }
    }
}
