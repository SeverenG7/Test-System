using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;
using System;

namespace TestSystem.Web.Models
{
    public class ResultViewModel
    {
        public IEnumerable<UserInfoDto> Users { get; set; }
        public IEnumerable<ResultDto> Results { get; set; }

        public ResultViewModel()
        {
            Users = new List<UserInfoDto>();
            Results = new List<ResultDto>();
        }
    }

    public class PremissionViewModel
    {
        public List<TestPremissionViewModel> Tests { get; set; }
        public ResultDto UserResult { get; set; }

        public PremissionViewModel()
        {
            Tests = new List<TestPremissionViewModel>();
            UserResult = new ResultDto();
        }
    }
}