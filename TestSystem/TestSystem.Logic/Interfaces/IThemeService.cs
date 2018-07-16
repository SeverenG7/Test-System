using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IThemeService
    {
        IEnumerable<ThemeDTO> GetThemes();
        ThemeDTO GetTheme(int id);
        void CreateTheme();
        void Dispose();
    }
}
