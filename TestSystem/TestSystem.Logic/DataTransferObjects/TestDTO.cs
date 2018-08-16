using System.Collections.Generic;
using System;

namespace TestSystem.Logic.DataTransferObjects
{
    public class TestDto
    {
        public int IdTest { get; set; }
        public string TestName { get; set; }
        public int QuestionsNumber { get; set; }
        public string TestDescription { get; set; }
        public string Difficult { get; set; }
        public DateTime CreateDate { get; set; }
        public int? IdTheme { get; set; }
        public ThemeDto Theme { get; set; }
        public virtual ICollection<QuestionDto> Questions { get; set; }
        public virtual ICollection<ResultDto> Result { get; set; }
        public TimeSpan Time { get; set; }
        public int TotalScore { get; set; }
        public byte[] TestImage { get; set; }
        public string ImageMimeType { get; set; }
    }
}
