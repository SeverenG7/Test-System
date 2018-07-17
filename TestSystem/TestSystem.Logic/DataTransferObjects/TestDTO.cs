using System.Collections.Generic;

namespace TestSystem.Logic.DataTransferObjects
{
    public class TestDTO
    {
        public int IdTest { get; set; }
        public string TestName { get; set; }
        public int QuestionsNumber { get; set; }
        public string TestDescription { get; set; }
        public int? IdProperty { get; set; }
        public virtual ICollection<QuestionDTO> Questions { get; set; }
    }
}
