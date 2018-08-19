using System;
using System.Collections.Generic;
using TestSystem.Model.Models;

namespace TestSystem.Logic.ViewModel
{
    public class ResultFullViewModel
    {
        public int IdResult { get; set; }
        public string IdUserInfo { get; set; }
        public int IdTest { get; set; }
        public double ResultScore { get; set; }
        public string ResultDescription { get; set; }
        public virtual TestViewModel Test { get; set; }
        public virtual UserInfoViewModel UserInfo { get; set; }
        public DateTime CreateDate { get; set; }
        public bool TestPassed { get; set; }
    }

    public class AnswerResultViewModel
    {
        public int IdAnswer { get; set; }
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
        public bool UserAnswer { get; set; }

        public AnswerResultViewModel(Answer answer, UserAnswer userAnswer)
        {
            IdAnswer = answer.IdAnswer;
            AnswerText = answer.AnswerText;
            Correct = answer.Correct;
            UserAnswer = userAnswer.Correct;
        }

    }

    public class QuestionResultViewModel
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public int Score { get; set; }
        public int UserScore { get; set; }
        public string Difficult { get; set; }
        public List<AnswerResultViewModel> Answers { get; set; }

        public QuestionResultViewModel(Question question, UserQuestion userQuestion)
        {
            IdQuestion = question.IdQuestion;
            QuestionText = question.QuestionText;
            Score = question.Score;
            UserScore = (int)userQuestion.UserScore;
            Difficult = question.Difficult;
            Answers = new List<AnswerResultViewModel>();
        }
    }

    public class ResultInfoViewModel
    {
        public Result Result { get; set; }
        public List<QuestionResultViewModel> Questions { get; set; }

        public ResultInfoViewModel(Result result , List<QuestionResultViewModel> questions)
        {
            Result = result;
            Questions = questions;
        }

        public ResultInfoViewModel()
        { }
            
    }

    public class ResultViewModel
    {
        public List<UserInfo> Users { get; set; }
        public List<Result> Results { get; set; }

        public ResultViewModel()
        {
            Users = new List<UserInfo>();
            Results = new List<Result>();
        }
    }

    public class PremissionViewModel
    {
        public List<TestPremissionViewModel> Tests { get; set; }
        public Result UserResult { get; set; }

        public PremissionViewModel()
        {
            Tests = new List<TestPremissionViewModel>();
            UserResult = new Result();
        }
    }

}
