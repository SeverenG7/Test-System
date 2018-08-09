using System.Collections.Generic;
using TestSystem.Logic.DataTransferObjects;

namespace TestSystem.Logic.Interfaces
{
    public interface IThemeService
    {
        IEnumerable<ThemeDto> GetAll();
        ThemeDto Get(int? id);
        void Create(ThemeDto themeDto);
        void Remove(int? id);
        void Update(ThemeDto theme);
    }
}
