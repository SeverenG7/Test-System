using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;

namespace TestSystem.Web.Models
{
    public class TestCreateViewModel
    {
        public string TestName { get; set; }

        [Required(ErrorMessage = "Every question must have its own difficult")]
        [Display(Name = "Difficult")]
        public IEnumerable<SelectListItem> Difficult { get; set; }
        public string selectedDifficult { get; set; }

        [Required(ErrorMessage = "Please, choose theme for this question or create new theme")]
        [Display(Name = "Question theme")]
        public IEnumerable<SelectListItem> Theme { get; set; }
        public string selectedTheme { get; set; }

        [DataType(DataType.MultilineText)]
        public string TestDescription { get; set; }

        public List<QuestionForTestViewModel> Questions { get; set; }
        public TestCreateViewModel()
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

    public class TestGenerateViewModel
    {
        public string TestName { get; set; }

        [Required(ErrorMessage = "Every question must have its own difficult")]
        [Display(Name = "Difficult")]
        public IEnumerable<SelectListItem> Difficult { get; set; }
        public string selectedDifficult { get; set; }

        [Required(ErrorMessage = "Please, choose theme for this question or create new theme")]
        [Display(Name = "Question theme")]
        public IEnumerable<SelectListItem> Theme { get; set; }
        public string selectedTheme { get; set; }

        [DataType(DataType.MultilineText)]
        public string TestDescription { get; set; }

        public List<QuestionDTO> Questions { get; set; }

        public IEnumerable<SelectListItem> NumberQuestions { get; set; }
        public int selectedNumber { get; set; }

        public TestGenerateViewModel()
        {
            Difficult = new SelectList(new List<string>()
            {
                "Junior",
                "Middle",
                "Senior"

            });

            List<int> list = new List<int>();
            for (int i = 1; i < 31; i++)
            {
                list.Add(i);
            }
            NumberQuestions = new SelectList(list);
            Theme = new SelectList(new List<ThemeDTO>());
            Questions = new List<QuestionDTO>();

        }
    }
    
}