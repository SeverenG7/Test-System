namespace TestSystem.Model.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  

    [Table("Theme")]
    public  class Theme
    {
        public Theme()
        {
            Tests = new HashSet<Test>();
            Questions = new HashSet<Question>();
        }

        [Key]
        public int IdTheme { get; set; }

        [Required]
        [StringLength(50)]
        public string ThemeName { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public virtual ICollection<Test> Tests { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

    }
}
