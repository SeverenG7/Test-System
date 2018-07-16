namespace TestSystem.Model.Models
{
   
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Answer")]
    public  class Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
