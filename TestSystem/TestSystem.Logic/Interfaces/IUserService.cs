using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Logic.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> CreateAsync(UserDto userDto);
        Task<OperationDetails> AuthenticateAsync(UserDto userDto);
        Task SetInitialDataAsync( List<string> roles);
        Task SendEmailAsync(string id, string theme, string reference);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<OperationDetails> ForgotPasswordAsync(string email);
        Task<OperationDetails> ResetPassworAsync(string email, string token, string password);

    }
}
