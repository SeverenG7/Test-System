using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using TestSystem.Model.Models;

namespace TestSystem.Logic.LogicView
{
    public class FiltrationViewModel
    {
        public IPagedList<Test> Tests { get; set; }
        public IPagedList<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public SelectList Themes { get; set; }
        public SelectList Difficult { get; set; }
        public int IdQuestion { get; set; }
        public int IdTest { get; set; }
        public string selectedDifficult { get; set; }
        public string selectedTheme { get; set; }
        public string searchString { get; set; }

        public FiltrationViewModel()
        {
            Difficult = new SelectList(new List<string>()
            {
                "All",
                "Junior",
                "Middle",
                "Senior"

            });
        }

    }
}