using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Infrastructure;
using TestSystem.DataProvider.Interfaces;
using TestSystem.Model.Models;
using TestSystem.Logic.DataTransferObjects;

using TestSystem.DataProvider.IdentityManager;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using Microsoft.Owin.Security.DataProtection;

namespace TestSystem.Logic.Services
{
    public class UserService : IUserService , IDisposable
    {
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


        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.ApplicationUserManagers.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.ApplicationUserManagers.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "" ,0 ,user.Id);
                // добавляем роль
              //  await Database.ApplicationUserManagers.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                UserInfo userInfo = new UserInfo { IdUserInfo = user.Id, UserFirstName = userDto.UserFirstName, UserLastName = userDto.UserLastName};
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

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.ApplicationUserManagers.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    claim = await Database.ApplicationUserManagers.CreateIdentityAsync(user,
                                               DefaultAuthenticationTypes.ApplicationCookie);
                    return claim;
                }
                else
                {
                    claim = await Database.ApplicationUserManagers.CreateIdentityAsync(user,
                                                DefaultAuthenticationTypes.ApplicationCookie);
                }
            }
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
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
            await Create(adminDto);
        }

        public async Task SendEmailAsync(string id , string reference)
        {
           await Database.ApplicationUserManagers.SendEmailAsync(id, "Подтверждение электронной почты",
                               "Для завершения регистрации перейдите по ссылке:: <a href=\""
                                                               + reference + "\">завершить регистрацию</a>");
        }



        public void Dispose()
        {
            Database.Dispose();
        }


        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            return await Database.ApplicationUserManagers.ConfirmEmailAsync(userId, token);
        }

    }
}

