using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;

namespace TestSystem.Web.Models
{
    public class ThemeCreateViewModels
    {
        [Required(ErrorMessage = "Please , put theme name!")]
        [Display(Name = "Theme name")]
        public string ThemeName { get; set; }

        [Required(ErrorMessage = "mYou should describe your new theme")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}