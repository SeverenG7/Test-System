using TestSystem.Logic.ViewModel;

namespace TestSystem.Logic.Interfaces
{
    public interface ICommonService
    {
        FiltrationViewModel FilterQuestions(int? IdTheme, string difficult, int? IdQuestion, int? IdTest, string search,
            int? page);
        FiltrationViewModel FilterTests(int? IdTheme, string difficult, int? IdQuestion, int? IdTest, string search,
           int? page);
    }
}
