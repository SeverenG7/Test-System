using System.Collections.Generic;

namespace TestSystem.Web.Models
{
    public class AnswerViewModel
    {

        public int IdAnswer { get; set; }
        public string AnswerText { get; set; }
        public bool Correct { get; set; }
        public virtual ICollection<QuestionViewModel> Questions { get; set; }
    }
}