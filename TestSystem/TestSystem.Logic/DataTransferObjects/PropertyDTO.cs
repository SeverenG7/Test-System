using System.Collections.Generic;

namespace TestSystem.Logic.DataTransferObjects
{
    public class PropertyDTO
    {
        public int IdProperty { get; set; }
        public string Difficult { get; set; }
        public int IdTheme { get; set; }
        public ThemeDTO Theme { get; set; }
        public virtual ICollection<QuestionDTO> Questions { get; set; }
        public virtual ICollection<TestDTO> Tests { get; set; }
    }
}
