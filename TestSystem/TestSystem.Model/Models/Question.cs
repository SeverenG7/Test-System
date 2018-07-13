namespace TestSystem.Model.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Question")]
    public  class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answer = new HashSet<Answer>();
            Test = new HashSet<Test>();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test> Test { get; set; }
    }
}
