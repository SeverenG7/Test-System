namespace TestSystem.Model.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Test")]
    public  class Test
    {
        public Test()
        {
            Result = new HashSet<Result>();
            Questions = new HashSet<Question>();
        }

        [Key]
        public int IdTest { get; set; }

        [Required]
        [StringLength(50)]
        public string TestName { get; set; }

        public int QuestionsNumber { get; set; }

        [Column(TypeName = "text")]
        public string TestDescription { get; set; }

        public int? IdProperty { get; set; }

        public virtual Property Property { get; set; }

        public virtual ICollection<Result> Result { get; set; }
     
        public virtual ICollection<Question> Questions { get; set; }
    }
}
