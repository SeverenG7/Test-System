namespace TestSystem.Model.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;

    [Table("Result")]
    public  class Result
    {
        [Key]
        public int IdResult { get; set; }

        [ForeignKey("UserInfo")]
        public string IdUserInfo { get; set; }

        [Required]
        [ForeignKey("Test")]
        public int IdTest { get; set; }

        public double? ResultScore { get; set; }

        [Column(TypeName = "text")]
        public string ResultDescription { get; set; }

        public virtual Test Test { get; set; }

        public virtual TempResult TempResult { get; set; }

        public virtual UserInfo UserInfo { get; set; }

        public DateTime CreateDate { get; set; }

        public bool TestPassed { get; set; }
    }
}
