using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Model.Models;

namespace TestSystem.Logic.ViewModel
{
    public class ThemeCreateViewModels
    {
        [Required(ErrorMessage = "Please , put theme name!")]
        [Display(Name = "Theme name")]
        public string ThemeName { get; set; }

        [Required(ErrorMessage = "mYou should describe your new theme")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }


    public class ThemeAboutViewModel
    {
        public IEnumerable<Theme> Themes { get; set; }
        public IEnumerable<Test> Tests { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public List<int> TestsNumber { get; set; }
        public List<int> QuestionsNumber { get; set; }

        public ThemeAboutViewModel()
        {
            TestsNumber = new List<int>();
            QuestionsNumber = new List<int>();
        }
    }

    public class ThemeViewModel
    {
        public int IdTheme { get; set; }
        public string ThemeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TestViewModel> Tests { get; set; }
        public virtual ICollection<QuestionViewModel> Questions { get; set; }
    }

}