using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Logic.DataTransferObjects;
using System.Web.Mvc;

namespace TestSystem.Web.Models
{
    public class UserMainViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Role { get; set; }

        public IEnumerable<ResultDTO> Results { get; set; }
        public TestDTO Test { get; set; }
    }
}