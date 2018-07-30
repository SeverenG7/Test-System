using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Logic.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto);
        Task SetInitialDataAsync(UserDTO adminDto, List<string> roles);
        Task SendEmailAsync(string id, string theme, string reference);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
        Task<OperationDetails> ForgotPasswordAsync(string email);
        Task<OperationDetails> ResetPassworAsync(string email, string token, string password);

    }
}
