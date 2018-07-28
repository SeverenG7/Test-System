namespace TestSystem.Model.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Question")]
    public  class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Tests = new HashSet<Test>();
        }

        [Key]
        public int IdQuestion { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string QuestionText { get; set; }

        [Column(TypeName = "image")]
        public byte[] QuestionImage { get; set; }

        public int AnswerNumber { get; set; }

        public int Score { get; set; }

        public int? IdProperty { get; set; }

        public virtual Property Property { get; set; }
   
        public virtual ICollection<Answer> Answers { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
