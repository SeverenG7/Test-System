using System.Collections.Generic;
using TestSystem.Model.Models;

namespace TestSystem.Logic.DataTransferObjects
{
    public class ThemeDTO
    {
        public int IdTheme { get; set; }
        public string ThemeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
