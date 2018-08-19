using System.Collections.Generic;
using TestSystem.Model.Models;

namespace TestSystem.Logic.ViewModel
{
    public class AnswerViewModel
    {
        public int IdAnswer { get; set; }

        public string AnswerText { get; set; }

        public bool Correct { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
