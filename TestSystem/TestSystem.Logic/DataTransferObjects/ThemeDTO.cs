using System.Collections.Generic;

namespace TestSystem.Logic.DataTransferObjects
{
    public class ThemeDTO
    {
        public int IdTheme { get; set; }
        public string ThemeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TestDTO> Tests { get; set; }
        public virtual ICollection<QuestionDTO> Questions { get; set; }
    }
}
