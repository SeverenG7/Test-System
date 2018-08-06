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
            //foreach (AnswerDTO ans in questionDTO.Answers)
            //{
            //    Answer answer = Database.Answers.Get(ans.IdAnswer);
            //    Database.Answers.Updating(answer);
            //}
            //Question question = Database.Questions.Get(questionDTO.IdQuestion);
            //Theme theme = Database.Themes.Get(questionDTO.IdTheme.Value);
            //Database.Questions.Updating(question);
            //Database.Themes.Updating(theme);


            //Question questionUpdate = MapperToDB.Map<Question>(questionDTO);
            //foreach (Answer ans in questionUpdate.Answers)
            //{
            //    Database.Answers.Update(ans);
            //}
            foreach (AnswerDTO ans in questionDTO.Answers)
            {
                Answer answer = Database.Answers.Get(ans.IdAnswer);
                answer.AnswerText = ans.AnswerText;
                answer.Correct = ans.Correct;
                Database.Answers.Update(answer);
            }
            Database.Questions.Update(Database.Questions.Get(questionDTO.IdQuestion));
            Database.Complete();
        }

        public static int ComputeScore(QuestionDTO question)
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
