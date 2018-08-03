using System.Collections.Generic;
using System;

namespace TestSystem.Logic.DataTransferObjects
{
    public class QuestionDTO
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public byte[] QuestionImage { get; set; }
        public int AnswerNumber { get; set; }
        public int Score { get; set; }
        public string Difficult { get; set; }
        public DateTime CreateDate { get; set; }
        public int? IdTheme { get; set; }
        public ThemeDTO Theme { get; set; }
        public virtual ICollection<AnswerDTO> Answers { get; set; }

        public QuestionDTO()
        { }
    }
}
