using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TestSystem.Model.Models
{
    [Table("UserQuestion")]
    public class UserQuestion
    {
        [Key]
        public int IdUserQuestion { get; set; }
        [Required]
        [ForeignKey("Result")]
        public int IdResult { get; set; }
        public int IdQuestion { get; set; }
        public double UserScore { get; set; }
        public int MaxScore { get; set; }
        public virtual Result Result { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }

        public UserQuestion()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }
    }
}
