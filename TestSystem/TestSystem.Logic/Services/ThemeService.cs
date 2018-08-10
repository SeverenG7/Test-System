using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Collections.Generic;


namespace TestSystem.Logic.Services
{
    public class ThemeService : MapClass<Theme,ThemeDto> , IThemeService
    {
        IUnitOfWork Database { get; set; }

        public ThemeService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public IEnumerable<ThemeDto> GetAll()
        {
            return MapperFromDB.Map<IEnumerable<Theme>, List<ThemeDto>>(Database.Themes.GetAll());
        }

        public ThemeDto Get(int? id)
        {
            Theme theme = Database.Themes.Get(id.Value);
            ThemeDto themeDTO = MapperFromDB.Map<ThemeDto>(theme);
            return themeDTO;
        }

        public void Create(ThemeDto themeDTO)
        {
            Theme theme = MapperToDB.Map<Theme>(themeDTO);
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

        public void Update(ThemeDto themeDTO)
        {
            Theme theme = Database.Themes.Get(themeDTO.IdTheme);
            Theme themeUpdate = MapperToDB.Map<Theme>(themeDTO);
            Database.Themes.Update(themeUpdate);
        }

    }
}
