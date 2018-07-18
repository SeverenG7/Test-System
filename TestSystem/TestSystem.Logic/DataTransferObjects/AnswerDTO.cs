﻿using System.Collections.Generic;

namespace TestSystem.Logic.DataTransferObjects
{
    public class AnswerDTO
    {
        public int IdAnswer { get; set; }
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
        public virtual ICollection<QuestionDTO> Questions { get; set; }
    }
}
