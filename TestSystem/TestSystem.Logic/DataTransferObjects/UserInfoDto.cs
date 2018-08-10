using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestSystem.Model.Models;

namespace TestSystem.Logic.DataTransferObjects
{
    public class UserInfoDto
    {
        public string IdUserInfo { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserRole { get; set; }
        public virtual ICollection<ResultDto> Result { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
