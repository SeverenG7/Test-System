using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;
using PagedList;

namespace TestSystem.Web.Models
{
    public class FiltrationViewModel
   {
        public IEnumerable<TestDto> Tests { get; set; }
        public SelectList Themes { get; set; }
        public SelectList Difficult { get; set; }
        public IPagedList<QuestionDto> Questions { get; set; }
        public IEnumerable<AnswerDto> Answers { get; set; }

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