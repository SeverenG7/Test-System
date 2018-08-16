using System.Collections.Generic;
using System;

namespace TestSystem.Logic.DataTransferObjects
{
    public class QuestionDto
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
        public ThemeDto Theme { get; set; }
        public virtual List<AnswerDto> Answers { get; set; }
        public virtual ICollection<TestDto> Tests { get; set; }
    }
}
