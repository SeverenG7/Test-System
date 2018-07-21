using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IThemeService
    {
        IEnumerable<ThemeDTO> GetAll();
        ThemeDTO Get(int? id);
        void Create(ThemeDTO themeDTO);
        void Remove(int? id);
        void Update(ThemeDTO theme);
        void Dispose();
    }
}
