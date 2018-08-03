using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;
namespace TestSystem.Web.Models
{
    public class QuestionCreateViewModel
    {
       [Required(ErrorMessage ="Question can not exist without some message for user!")]
       [Range(5,200 , ErrorMessage ="Question text must have minimum 5 words and maximum - 200")]
       [Display(Name ="Question text")]
       [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }

       [Display(Name = "Question image")]
       public byte[] QuestionImage { get; set; }

       [Required(ErrorMessage = "Every question must have its own difficult")]
        [Display(Name = "Difficult")]
        public IEnumerable<SelectListItem> Difficult { get; set; }

        public string selectedDifficult { get; set; }
   
 
       [Required(ErrorMessage = "Please, choose theme for this question or create new theme")]
        [Display(Name = "Question theme")]
        public IEnumerable<SelectListItem>  Theme { get; set; }

       
        public string selectedTheme { get; set; }
       

        [Required(ErrorMessage = "Every question must have answers")]
        [Range(2, 5, ErrorMessage = "In question you can place from 2 to 5 answers")]
        public List<AnswerDTO> Answers { get; set; }

        public QuestionCreateViewModel()
        {
            Difficult = new SelectList(new List<string>()
            {
                "Junior",
                "Middle",
                "Senior"

            });


            Theme = new SelectList(new List<ThemeDTO>());

        }

    }

    public class QuestionViewModel
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public string Theme { get; set; }
        public string Difficult { get; set; }
        public int Score { get; set; }

    }

    public class QuestionForTestViewModel
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public string Theme { get; set; }
        public string Difficult { get; set; }
        public bool Chosen { get; set; }

    }




}


