using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TestSystem.Web.Models
{
    public class TestViewModel
    {
        public int IdTest { get; set; }
        [DisplayName("Test name")]
        public string TestName { get; set; }
        [DisplayName("Questions number")]
        public int QuestionsNumber { get; set; }
        [DisplayName("Description")]
        public string TestDescription { get; set; }
        public int? IdProperty { get; set; }
    }

}