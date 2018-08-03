using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;

namespace TestSystem.Web.Models
{
    public class FiltrationViewModel
   {
        public IEnumerable<TestDTO> Tests { get; set; }
        public SelectList Themes { get; set; }
        public SelectList Difficult { get; set; }
        public IEnumerable<QuestionDTO> Questions { get; set; }
        public IEnumerable<AnswerDTO> Answers { get; set; }
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