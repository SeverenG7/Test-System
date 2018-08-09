using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestSystem.Logic.DataTransferObjects
{
    public class AnswerDto
    {
        public int IdAnswer { get; set; }
        [DataType(DataType.MultilineText)]
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
        public virtual ICollection<QuestionDto> Questions { get; set; }
    }
}
