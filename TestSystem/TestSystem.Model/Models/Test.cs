namespace TestSystem.Model.Models
{
    using System;
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

        public virtual ICollection<Result> Result { get; set; }
     
        public virtual ICollection<Question> Questions { get; set; }

        public int? IdTheme { get; set; }
        public Theme Theme { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public string Difficult { get; set; }
        public TimeSpan Time { get; set; }
    }
}
