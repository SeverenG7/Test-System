using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;
using System;

namespace TestSystem.Web.Models
{
    public class ResultViewModel
    {
        public string UserLogin { get; set; }

        public double ResultScore { get; set; }

        public virtual TestDto Test { get; set; }

        public DateTime CreateDate { get; set; }

    }
}