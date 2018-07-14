namespace TestSystem.Logic.DataTransferObjects
{
    public class QuestionDTO
    {
        public int IdQuestion { get; set; }
        public string QuestionText { get; set; }
        public byte[] QuestionImage { get; set; }
        public int AnswerNumber { get; set; }
        public int Score { get; set; }
        public int? IdProperty { get; set; }
    }
}
