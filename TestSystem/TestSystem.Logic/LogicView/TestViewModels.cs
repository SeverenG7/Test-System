﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;

namespace TestSystem.Web.Models
{
    public class TestCreateViewModel
    {
        [Required(ErrorMessage = "Add name to test")]
        public string TestName { get; set; }
        [Display(Name = "Difficult")]
        public IEnumerable<SelectListItem> Difficult { get; set; }
        public string selectedDifficult { get; set; }
        [Display(Name = "Question theme")]
        public IEnumerable<SelectListItem> Theme { get; set; }
        public string selectedTheme { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Add few words about this test")]
        [StringLength(200, MinimumLength = 10,
        ErrorMessage = "Test descriptions must be between 10 and 200 characters in length.")]
        public string TestDescription { get; set; }
        public List<QuestionForTestViewModel> Questions { get; set; }
        public IEnumerable<SelectListItem> Time { get; set; }
        [Required(ErrorMessage = "Add time for passing test")]
        [Range(1, 35,
        ErrorMessage = "Time for {0} must be between {1} and {2}.")]
        public int selectedTime { get; set; }
        public TestCreateViewModel()
        {
            Difficult = new SelectList(new List<string>()
            {
                "Junior",
                "Middle",
                "Senior"

            });
            Theme = new SelectList(new List<ThemeDto>());
            List<int> list = new List<int>();
            for (int i = 1; i < 31; i++)
            {
                list.Add(i);
            }
            Time = new SelectList(list);
            Theme = new SelectList(new List<ThemeDto>());
            selectedTime = 10;
        }

    }

    public class TestGenerateViewModel
    {
        public string TestName { get; set; }

        [Required(ErrorMessage = "Every question must have its own difficult")]
        [Display(Name = "Difficult")]
        public IEnumerable<SelectListItem> Difficult { get; set; }
        public string selectedDifficult { get; set; }

        [Required(ErrorMessage = "Please, choose theme for this question or create new theme")]
        [Display(Name = "Question theme")]
        public IEnumerable<SelectListItem> Theme { get; set; }
        public string selectedTheme { get; set; }

        [DataType(DataType.MultilineText)]
        public string TestDescription { get; set; }

        public List<QuestionDto> Questions { get; set; }

        public IEnumerable<SelectListItem> NumberQuestions { get; set; }
        public int selectedNumber { get; set; }
        public bool Create { get; set; }

        public TestGenerateViewModel()
        {
            Difficult = new SelectList(new List<string>()
            {
                "Junior",
                "Middle",
                "Senior"

            });

            List<int> list = new List<int>();
            for (int i = 1; i < 31; i++)
            {
                list.Add(i);
            }
            NumberQuestions = new SelectList(list);
            Theme = new SelectList(new List<ThemeDto>());
            Questions = new List<QuestionDto>();

        }
    }

    public class TestPremissionViewModel
    {
        public int IdTest { get; set; }
        public string TestName { get; set; }
        public int QuestionsNumber { get; set; }
        public string TestDescription { get; set; }
        public string Difficult { get; set; }
        public ThemeDto Theme { get; set; }
        public bool Choosen { get; set; }
    }
}