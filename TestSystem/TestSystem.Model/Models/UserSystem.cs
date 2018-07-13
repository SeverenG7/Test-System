namespace TestSystem.Model.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserSystem")]
    public partial class UserSystem
    {
        [Key]
        public int IdUser { get; set; }

        public int IdUserInfo { get; set; }

        [StringLength(50)]
        public string UserPassword { get; set; }

        public bool? UserRights { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
