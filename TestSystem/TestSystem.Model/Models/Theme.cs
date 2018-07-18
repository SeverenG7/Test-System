namespace TestSystem.Model.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  

    [Table("Theme")]
    public  class Theme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Theme()
        {
            Properties = new HashSet<Property>();
        }

        [Key]
        public int IdTheme { get; set; }

        [Required]
        [StringLength(50)]
        public string ThemeName { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Property> Properties { get; set; }
    }
}
