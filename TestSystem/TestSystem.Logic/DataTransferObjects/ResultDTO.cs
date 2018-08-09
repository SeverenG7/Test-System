using System;

namespace TestSystem.Logic.DataTransferObjects
{
    public class ResultDto
    {
        public int IdResult { get; set; }

        public string UserLogin { get; set; }

        public int IdTest { get; set; }

        public double ResultScore { get; set; }

        public string ResultDescription { get; set; }

        public virtual TestDto Test { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
