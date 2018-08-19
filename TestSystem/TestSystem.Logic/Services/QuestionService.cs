using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Model.Models;
using TestSystem.Logic.ViewModel;
using System.Web.Mvc;
using System.Web;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.MapGeneric;

namespace TestSystem.Logic.Services
{
    public class QuestionService : MapClass<Answer,AnswerViewModel>, IQuestionService
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
                newQuestion.Answers = new List<AnswerViewModel>();
                for (int i = 0; i < 5; i++)
                {
                    newQuestion.Answers.Add(new AnswerViewModel());
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
                        QuestionText = question.QuestionText,
                        Answers = MapperFromDB.Map<IEnumerable<Answer>,List<AnswerViewModel>>(question.Answers),
                        IdQuestion = question.IdQuestion,
                        ImageMimeType = question.ImageMimeType,
                        QuestionImage = question.QuestionImage
                    };
                    if (question.Theme == null)
                    {
                        updateQuestion.selectedTheme = "no theme";
                    }
                    else
                    {
                        updateQuestion.selectedTheme = question.Theme.ThemeName;
                    }
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

            foreach(AnswerViewModel ans in model.Answers)
            {
                if (!String.IsNullOrEmpty(ans.AnswerText))
                {
                    question.Answers.Add(MapperToDB.Map<AnswerViewModel, Answer>
                        (ans));
                }
            }
            if (image != null)
            {
                question.ImageMimeType = image.ContentType;
                question.QuestionImage = new byte[image.ContentLength];
                image.InputStream.Read(question.QuestionImage, 0, image.ContentLength);
            }
            question.AnswerNumber = question.Answers.Count;
            question.Score = ComputeScore(question);
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

        public void UpdateQuestion(QuestionCreateViewModel questionDTO , HttpPostedFileBase image)
        {
            Question question = Database.Questions.Get(questionDTO.IdQuestion);
            question.Difficult = questionDTO.selectedDifficult;
            question.QuestionText = questionDTO.QuestionText;
            question.Theme = Database.Themes.Get(Int32.Parse(questionDTO.selectedTheme));
            foreach(AnswerViewModel ans in questionDTO.Answers)
            {
                Answer answer = Database.Answers.Get(ans.IdAnswer);
                answer.AnswerText = ans.AnswerText;
                answer.Correct = ans.Correct;
                Database.Answers.Update(answer);
            }
            if (image != null)
            {
                question.ImageMimeType = image.ContentType;
                question.QuestionImage = new byte[image.ContentLength];
                image.InputStream.Read(question.QuestionImage, 0, image.ContentLength);
            }

            question.AnswerNumber = question.Answers.Count;
            question.Score = ComputeScore(question);
            Database.Questions.Update(question);
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

        public  int ComputeScore(Question question)
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
