using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System;
using System.Linq;
using System.Web.Mvc;

namespace TestSystem.Web.Models
{
    public class FiltrationTestViewModel
   {
        public IEnumerable<TestDTO> Tests { get; set; }
        public SelectList Themes { get; set; }
        public SelectList Difficult { get; set; }
        public IEnumerable<QuestionDTO> Questions { get; set; }
        public IEnumerable<AnswerDTO> Answers { get; set; }
        public FiltrationTestViewModel()
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