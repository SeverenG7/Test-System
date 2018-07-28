namespace TestSystem.Model.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserInfo")]
    public  class UserInfo
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string IdUserInfo { get; set; }

        [Required]
        [StringLength(20)]
        public string UserFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string UserLastName { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
