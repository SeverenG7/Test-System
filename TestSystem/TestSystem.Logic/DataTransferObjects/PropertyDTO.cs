using System.Collections.Generic;
using TestSystem.Model.Models;

namespace TestSystem.Logic.DataTransferObjects
{
    public class PropertyDTO
    {
        public int IdProperty { get; set; }
        public int Difficult { get; set; }
        public int IdTheme { get; set; }
        public Theme GetTheme { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
