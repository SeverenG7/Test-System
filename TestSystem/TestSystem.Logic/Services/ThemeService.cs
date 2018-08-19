using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.ViewModel;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TestSystem.Logic.Services
{
    public class ThemeService : MapClass<Theme,ThemeViewModel> , IThemeService
    {
        IUnitOfWork Database { get; set; }

        public ThemeService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public IEnumerable<ThemeViewModel> GetAll()
        {
            return MapperFromDB.Map<IEnumerable<Theme>, List<ThemeViewModel>>(Database.Themes.GetAll());
        }

        public ThemeViewModel Get(int? id)
        {
            Theme theme = Database.Themes.Get(id.Value);
            ThemeViewModel themeDTO = MapperFromDB.Map<ThemeViewModel>(theme);
            return themeDTO;
        }

        public void Create(ThemeCreateViewModels model)
        {
            Theme theme = new Theme
            {
                ThemeName = model.ThemeName,
                Description = model.Description
            };
            Database.Themes.Add(theme);
            Database.Complete();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Remove(int? id)
        {
            Theme theme = Database.Themes.Get(id.Value);
            if (theme != null)
            {
                Database.Themes.Remove(theme);
                Database.Complete();
            }
        }

        public void Update(ThemeViewModel themeDTO)
        {
            Theme theme = Database.Themes.Get(themeDTO.IdTheme);
            Theme themeUpdate = MapperToDB.Map<Theme>(themeDTO);
            Database.Themes.Update(themeUpdate);
        }

        public ThemeAboutViewModel AboutThemes(int? IdTheme, string search)
        {
            ThemeAboutViewModel modelView = new ThemeAboutViewModel();
            modelView.Themes = Database.Themes.GetAll();

            if (!String.IsNullOrEmpty(search))
            {
                modelView.Themes = modelView.Themes.Where(x => x.ThemeName.Contains(search));
            }

            if (IdTheme.HasValue)
            {
                if (modelView.Themes.Where(x => x.IdTheme == IdTheme) != null)
                {
                    modelView.Tests = Database.Tests.GetAll().
                        Where(x => x.IdTheme == IdTheme.Value).ToList();

                    modelView.Questions = Database.Questions.GetAll().
                        Where(x => x.IdTheme == IdTheme.Value).ToList();
                }
            }
            return modelView;
        }

    }
}
