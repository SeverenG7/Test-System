using TestSystem.DataProvider.Interfaces;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Model.Models;
using TestSystem.Logic.MapGeneric;
using System.Collections.Generic;


namespace TestSystem.Logic.Services
{
    public class ThemeService : MapClass<Theme,ThemeDTO> , IThemeService
    {
        IUnitOfWork Database { get; set; }

        public ThemeService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public IEnumerable<ThemeDTO> GetAll()
        {
            return MapperFromDB.Map<IEnumerable<Theme>, List<ThemeDTO>>(Database.Themes.GetAll());
        }

        public ThemeDTO Get(int? id)
        {
            Theme theme = Database.Themes.Get(id.Value);
            ThemeDTO themeDTO = MapperFromDB.Map<ThemeDTO>(theme);
            return themeDTO;
        }

        public void Create(ThemeDTO themeDTO)
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

        public void Update(ThemeDTO themeDTO)
        {
            Theme theme = Database.Themes.Get(themeDTO.IdTheme);
            Database.Themes.Updating(theme);
            Theme themeUpdate = MapperToDB.Map<Theme>(themeDTO);
            Database.Themes.Update(themeUpdate);
        }

    }
}
