using System.Collections.Generic;
using TestSystem.Model.Models;

namespace TestSystem.Logic.DataTransferObjects
{
    public class TestDTO
    {
        public int IdTest { get; set; }
        public string TestName { get; set; }
        public int QuestionsNumber { get; set; }
        public string TestDescription { get; set; }
        public int? IdProperty { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
