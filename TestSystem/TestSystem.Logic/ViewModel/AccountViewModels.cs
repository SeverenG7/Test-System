using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestSystem.Model.Models;


namespace TestSystem.Logic.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Role { get; set; }
    }

    public class UserMainViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Role { get; set; }
        public IEnumerable <Result> Results { get; set; }
        public Test Test { get; set; }
    }

    public class UserInfoViewModel
    {
        public string IdUserInfo { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserRole { get; set; }
        public virtual ICollection<ResultViewModel> Result { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please, put your email adress.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must put password to log in into the system.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please, fill field with your name")]
        [Display(Name = "Name")]
        [StringLength(20, MinimumLength = 4,
        ErrorMessage = "Your name should have from 4 to 20 letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please, fill field with your lastname")]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please, put your email adress. It will help you, if you forgot yor password.")]
        [EmailAddress(ErrorMessage = "Please, put email in full, correct style.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Put password, to protect your account")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Please, put your email adress. It will help you, if you forgot yor password.")]
        [EmailAddress(ErrorMessage = "Please, put email in full, correct style.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Put new password.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please, put your email adress, which you used for registration.")]
        [EmailAddress(ErrorMessage = "Please, put email in full, correct style.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
