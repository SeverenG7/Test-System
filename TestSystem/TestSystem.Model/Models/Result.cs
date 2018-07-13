namespace TestSystem.Model.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Result")]
    public  class Result
    {
        [Key]
        public int IdResult { get; set; }

        [Required]
        [StringLength(20)]
        public string UserLogin { get; set; }

        public int IdTest { get; set; }

        public double ResultScore { get; set; }

        [Column(TypeName = "text")]
        public string ResultDescription { get; set; }

        public virtual Test Test { get; set; }
    }
}
