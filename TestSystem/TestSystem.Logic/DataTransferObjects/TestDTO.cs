using System.Collections.Generic;
using System;

namespace TestSystem.Logic.DataTransferObjects
{
    public class TestDTO
    {
        public int IdTest { get; set; }
        public string TestName { get; set; }
        public int QuestionsNumber { get; set; }
        public string TestDescription { get; set; }
        public string Difficult { get; set; }
        public DateTime CreateDate { get; set; }
        public int? IdTheme { get; set; }
        public ThemeDTO Theme { get; set; }
        public virtual ICollection<QuestionDTO> Questions { get; set; }
        public virtual ICollection<ResultDTO> Result { get; set; }
    }
}
