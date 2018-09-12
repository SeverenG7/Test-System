using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestSystem.Model.Models
{
    [Table("UserAnswer")]
    public class UserAnswer
    {
        [Key]
        public int IdUserAnswer { get; set; }
        [Required]
        [ForeignKey("UserQuestion")]
        public int IdUserQuestion { get; set; }
        public int IdAnswer { get; set; }
        public bool Correct { get; set; }
        public virtual UserQuestion UserQuestion { get; set; }
    }
}
