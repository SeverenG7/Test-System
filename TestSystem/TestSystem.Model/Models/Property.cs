namespace TestSystem.Model.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Property")]
    public  class Property
    {
        public Property()
        {
            Questions = new HashSet<Question>();
            Tests = new HashSet<Test>();
        }

        [Key]
        public int IdProperty { get; set; }

        public int Difficult { get; set; }

        public int IdTheme { get; set; }

        public virtual Theme Theme { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
