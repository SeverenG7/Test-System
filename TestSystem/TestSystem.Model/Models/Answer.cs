namespace TestSystem.Model.Models
{
   
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Answer")]
    public  class Answer
    {
        public Answer()
        {
            Questions = new HashSet<Question>();
        }

        [Key]
        public int IdAnswer { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string AnswerText { get; set; }

        public bool Correct { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
