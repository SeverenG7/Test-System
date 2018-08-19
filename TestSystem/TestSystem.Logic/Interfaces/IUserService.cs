using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestSystem.Logic.ViewModel;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Logic.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> CreateAsync(UserViewModel userDto);
        Task<OperationDetails> AuthenticateAsync(UserViewModel userDto);
        Task SetInitialDataAsync( List<string> roles);
        Task SendEmailAsync(string id, string theme, string reference);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<OperationDetails> ForgotPasswordAsync(string email);
        Task<OperationDetails> ResetPassworAsync(string email, string token, string password);
        string FindIdUser(string userName);
        UserMainViewModel MainMenuUser(int? id);
    }
}
