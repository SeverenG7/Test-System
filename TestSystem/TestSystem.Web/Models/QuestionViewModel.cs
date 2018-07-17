using System.Collections.Generic;

namespace TestSystem.Web.Models
{
    public class QuestionViewModel
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public byte[] QuestionImage { get; set; }
        public int AnswerNumber { get; set; }
        public int Score { get; set; }
        public int? IdProperty { get; set; }
        public virtual ICollection<AnswerViewModel> Answers { get; set; }
    }
}