using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using Microsoft.Owin.Security.DataProtection;
using System.Security.Claims;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Infrastructure;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Model.Models;
using TestSystem.Logic.DataTransferObjects;


namespace TestSystem.Logic.Services
{
    public class UserService : IUserService , IDisposable
    {

        #region Infrastructure
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
            Database.ApplicationUserManagers.EmailService = new EmailService();
            var provider = new DpapiDataProtectionProvider("TestSystem");
            Database.ApplicationUserManagers.UserTokenProvider 
                = new DataProtectorTokenProvider<ApplicationUser>(
                provider.Create("SampleTokenName"));

        }

        #endregion

        #region Methods
        public async Task<OperationDetails> CreateAsync(UserDto userDto)
        {
            ApplicationUser user = await Database.ApplicationUserManagers.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.ApplicationUserManagers.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "" ,0 ,user.Id);
                await Database.ApplicationUserManagers.AddToRoleAsync(user.Id, userDto.Role);

                UserInfo userInfo = new UserInfo { IdUserInfo = user.Id, UserFirstName = userDto.UserFirstName, UserLastName = userDto.UserLastName ,
                UserRole = userDto.Role};
                Database.UserInfoes.Add(userInfo);
                await Database.SaveAsync();

                var code = await Database.ApplicationUserManagers.GenerateEmailConfirmationTokenAsync(user.Id);
                return new OperationDetails(true, "На указанную почту выслано письмо с ссылкой " +
                    "для потверждения действий", "", code , user.Id);
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email" ,0 ,user.Id);
            }


           
        }

        public async Task<OperationDetails> AuthenticateAsync(UserDto userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.ApplicationUserManagers.FindAsync(userDto.Email, userDto.Password);
       

            if (user != null)
            {
                string us = user.Id;
                if (user.EmailConfirmed)
                {
                    UserInfo role = Database.UserInfoes.Find(x => x.IdUserInfo == user.Id).FirstOrDefault();
                    
                    claim = await Database.ApplicationUserManagers.CreateIdentityAsync(user,
                                               DefaultAuthenticationTypes.ApplicationCookie);
                    return new OperationDetails(true, role.UserRole, "", claim, "");
                }
                else
                {
                    claim = await Database.ApplicationUserManagers.CreateIdentityAsync(user,
                                                DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
            return new OperationDetails(true, "", "", claim, "");
        }

        public async Task SetInitialDataAsync( List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.ApplicationRoleManagers.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.ApplicationRoleManagers.CreateAsync(role);
                }
            }
        }
        public async Task SendEmailAsync(string id , string theme , string reference)
        {
           await Database.ApplicationUserManagers.SendEmailAsync(id, theme,reference);
        }
        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            return await Database.ApplicationUserManagers.ConfirmEmailAsync(userId, token);
        }
        public async Task<OperationDetails> ForgotPasswordAsync(string email )
        {
            var user = await Database.ApplicationUserManagers.FindByNameAsync(email);
            if (user != null || !(await Database.ApplicationUserManagers.IsEmailConfirmedAsync(user.Id)))
            {
                string code = await Database.ApplicationUserManagers.GeneratePasswordResetTokenAsync(user.Id);
                return new OperationDetails(true, "", "Email", code ,user.Id);
            }
            else
            {
                return new OperationDetails(false, "", "", "", "");
            }
        }
        public async Task<OperationDetails> ResetPassworAsync(string email , string token , string password)
        {
            var user = await Database.ApplicationUserManagers.FindByEmailAsync(email);
            if (user == null)
            {
                return new OperationDetails(false,"There is no user with such email" , "Reset password");
            }
            else
            {
                var result = await Database.ApplicationUserManagers.ResetPasswordAsync
                    (user.Id.ToString(), token, password);
                return new OperationDetails(result.Succeeded, result.Errors.ToString(), "Reset password");
            }
        }


        public string FindIdUser(string userName)
        {
            var user = Database.ApplicationUserManagers.FindByEmail(userName);
            return user.Id;
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        #endregion


        #region Custom methods

        public UserDto GetUserInfo(string userName)
        {
            UserDto user = new UserDto();
            var somebody = Database.UserInfoes.Find(x => x.ApplicationUser.Email == userName).First();
            if ( somebody != null)
            {
                  user = new UserDto
                {
                    Id = somebody.ApplicationUser.Id,
                    Email = userName,
                    Role = somebody.ApplicationUser.Roles.FirstOrDefault().ToString(),
                };
            }

            return user;
        }

        #endregion
    }
}

