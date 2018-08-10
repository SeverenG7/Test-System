namespace TestSystem.Model.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;

    [Table("TempResult")]
    public class TempResult
    {
        [Key]
        [ForeignKey("Result")]
        public int IdResult { get; set; }

        public virtual Result Result { get; set; }

        public string QuestionsPassed { get; set; }

        public string QuestionPassing { get; set; }

        public double TotalScore { get; set; }
    }
}
