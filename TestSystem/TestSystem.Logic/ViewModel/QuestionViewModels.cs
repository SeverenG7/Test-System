using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System;
using TestSystem.Model.Models;

namespace TestSystem.Logic.ViewModel
{
    public class QuestionCreateViewModel
    {
        public int IdQuestion { get; set; }
        [Required(ErrorMessage ="Question can not exist without some message for user!")]
        [Display(Name ="Question text")]
        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }

        [Required(ErrorMessage = "Every question must have its own difficult")]
        [Display(Name = "Difficult")]
        public IEnumerable<SelectListItem> Difficult { get; set; }
        public string selectedDifficult { get; set; }
   
 
        [Required(ErrorMessage = "Please, choose theme for this question or create new theme")]
        [Display(Name = "Question theme")]
        public IEnumerable<SelectListItem>  Theme { get; set; }       
        public string selectedTheme { get; set; }
       
        public List<AnswerViewModel> Answers { get; set; }

        public byte[] QuestionImage { get; set; }
        public string ImageMimeType { get; set; }

        public QuestionCreateViewModel()
        {
            Difficult = new SelectList(new List<string>()
            {
                "Junior",
                "Middle",
                "Senior"

            });


            Theme = new SelectList(new List<Theme>());

        }
    }

    public class QuestionFewViewModel
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public string Theme { get; set; }
        public string Difficult { get; set; }
        public int Score { get; set; }

    }

    public class QuestionViewModel
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public byte[] QuestionImage { get; set; }
        public string ImageMimeType { get; set; }
        public int AnswerNumber { get; set; }
        public int Score { get; set; }
        public string Difficult { get; set; }
        public DateTime CreateDate { get; set; }
        public int? IdTheme { get; set; }
        public Theme Theme { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }

    public class QuestionForTestViewModel
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public string Theme { get; set; }
        public string Difficult { get; set; }
        public int Score { get; set; }
        public bool Chosen { get; set; }

    }

    public class QuestionDetailsViewModel
    {
        public string QuestionText { get; set; }
        public int Score { get; set; }
        public string Difficult { get; set; }
        public DateTime CreateDate { get; set; }
        public  string Theme { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public virtual List<Test> Tests { get; set; }

        public QuestionDetailsViewModel(Question question)
        {
            QuestionText = question.QuestionText;
            Score = question.Score;
            CreateDate = question.CreateDate;
            Difficult = question.Difficult;
            Answers = question.Answers.ToList();
            Tests = new List<Test>();
        }
    }
}


