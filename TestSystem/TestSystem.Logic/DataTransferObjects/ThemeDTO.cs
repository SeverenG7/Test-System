using System.Collections.Generic;

namespace TestSystem.Logic.DataTransferObjects
{
    public class ThemeDto
    {
        public int IdTheme { get; set; }
        public string ThemeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TestDto> Tests { get; set; }
        public virtual ICollection<QuestionDto> Questions { get; set; }
    }
}
