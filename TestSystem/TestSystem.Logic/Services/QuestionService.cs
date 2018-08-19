using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Model.Models;
using TestSystem.Logic.ViewModel;
using System.Web.Mvc;
using System.Web;
using TestSystem.Logic.Interfaces;

namespace TestSystem.Logic.Services
{
    public class QuestionService : IQuestionService
    {
        #region Infrastructure
        private  IUnitOfWork Database { get;}
        public QuestionService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        #endregion

        #region Methods
        public QuestionCreateViewModel GetCreationModel(int? id)
        {
            if (!id.HasValue)
            {
                QuestionCreateViewModel newQuestion = new QuestionCreateViewModel();
                newQuestion.Theme = new SelectList(Database.Themes.GetAll(), "IdTheme", "ThemeName");
                newQuestion.Answers = new List<Answer>();
                for (int i = 0; i < 5; i++)
                {
                    newQuestion.Answers.Add(new Answer());
                }
                return newQuestion;
            }
            else
            {
                if (Database.Questions.Get(id.Value) != null)
                {
                    Question question = Database.Questions.Get(id.Value);
                    QuestionCreateViewModel updateQuestion = new QuestionCreateViewModel
                    {
                        selectedDifficult = question.Difficult,
                        selectedTheme = question.Theme.ThemeName,
                        QuestionText = question.QuestionText,
                        Answers = question.Answers.ToList(),
                        IdQuestion = question.IdQuestion
                    };
                    updateQuestion.Theme = new SelectList(Database.Themes.GetAll(), "IdTheme", "ThemeName");
                    return updateQuestion;
                }
                else
                {
                    return null;
                }
            }
        }

        public void CreateQuestion(QuestionCreateViewModel model , HttpPostedFileBase image)
        {
            Question question = new Question
            {
                QuestionText = model.QuestionText,
                Difficult = model.selectedDifficult,
                IdTheme = int.Parse(model.selectedTheme),
                CreateDate = DateTime.Now
            };

            foreach (Answer ans in model.Answers)
            {
                if (!String.IsNullOrEmpty(ans.AnswerText))
                {
                    question.Answers.Add(ans);
                }
            }
            if (image != null)
            {
                question.ImageMimeType = image.ContentType;
                question.QuestionImage = new byte[image.ContentLength];
                image.InputStream.Read(question.QuestionImage, 0, image.ContentLength);
            }
            question.AnswerNumber = question.Answers.Count;

            Database.Questions.Add(question);
            Database.Answers.AddRange(question.Answers);
            Database.Complete();
        }

        public QuestionViewModel GetQuestion(int? id)
        {
            Question question = Database.Questions.Get(id.Value);
            QuestionViewModel questionView = new QuestionViewModel
            {
                QuestionImage = question.QuestionImage,
                QuestionText = question.QuestionText,
                IdQuestion = question.IdQuestion,
                Answers = question.Answers.ToList(),
                Score = question.Score,
                AnswerNumber = question.AnswerNumber,
                CreateDate = question.CreateDate,
                Difficult = question.Difficult,
                IdTheme = question.IdTheme,
                ImageMimeType = question.ImageMimeType,
                Tests = question.Tests,
                Theme = question.Theme
            };
            return questionView;
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

        public void UpdateQuestion(QuestionCreateViewModel questionDTO)
        {
            Question question = Database.Questions.Get(questionDTO.IdQuestion);

            foreach (Answer ans in questionDTO.Answers)
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

        public IEnumerable<QuestionViewModel> GetLastQuestions()
        {
            List<Question> questions = Database.Questions.GetAll().
                OrderByDescending(x => x.CreateDate).
                Take(5).ToList();

            List<QuestionViewModel> lastQuestions = new List<QuestionViewModel>();

            foreach (Question question in questions)
            {
                lastQuestions.Add(new QuestionViewModel
                {
                    QuestionText = question.QuestionText,
                    IdQuestion = question.IdQuestion
                });
            }

            return lastQuestions;
        }

        public  int ComputeScore(QuestionViewModel question)
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

        public void Dispose()
        {
            Database.Dispose();
        }

        #endregion
    }
}
