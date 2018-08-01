using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Web.Models
{
    public class QuestionCreateViewModel
    {
       [Required(ErrorMessage ="Question can not exist without some message for user!")]
       [Range(5,200 , ErrorMessage ="Question text must have minimum 5 words and maximum - 200")]
       [Display(Name ="Question text")]
       public string QuestionText { get; set; }

       [Display(Name = "Question image")]
       public byte[] QuestionImage { get; set; }

       [Required(ErrorMessage = "Every question must have its own difficult")]
       [Display(Name ="Difficult")]
       public string Difficult { get; set; }
 
       [Required(ErrorMessage ="Please, choose theme for this question or create new theme")]
       [Display(Name ="Question theme")]
       public ThemeDTO Theme { get; set; }

        [Required(ErrorMessage ="Every question must have answers")]
        [Range(2,5 , ErrorMessage = "In question you can place from 2 to 5 answers")]
       public List<AnswerDTO> Answers { get; set; }
   
    }

    public class CommonViewModel
    {
        public IEnumerable<TestDTO> Tests { get; set; }
        public IEnumerable<QuestionDTO> Questions { get; set; }
        public IEnumerable<AnswerDTO> Answers { get; set; }
    }
}