using System.Collections.Generic;
using TestSystem.Logic.ViewModel;

namespace TestSystem.Logic.Interfaces
{
    public interface IThemeService
    {
        IEnumerable<ThemeViewModel> GetAll();
        ThemeViewModel Get(int? id);
        void Create(ThemeCreateViewModels themeDto);
        void Remove(int? id);
        void Update(ThemeViewModel theme);
        ThemeAboutViewModel AboutThemes(int? IdTheme, string search);
    }
}
