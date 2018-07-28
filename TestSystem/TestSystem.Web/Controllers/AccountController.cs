using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using TestSystem.Web.Models;
using TestSystem.Logic.DataTransferObjects;
using System.Security.Claims;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.Infrastructure;
using System.Data.Entity.Validation;

namespace UserStore.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService { get; set; }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IUserService userService)
        {
            UserService = userService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            //await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                try
                {
                    UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                    ClaimsIdentity claim = await UserService.Authenticate(userDto);
                    if (claim == null)
                    {
                        ModelState.AddModelError("", "Неверный логин или пароль , или не подтвержден e-mail");
                    }
                    else
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        return RedirectToAction("CommonTables", "Common");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Response.Write("Object: " + validationError.Entry.Entity.ToString());
                        Response.Write("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Response.Write(err.ErrorMessage + "");
                        }
                    }
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
           // await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    UserFirstName = "Nick",
                    UserLastName = "Chernyak"
                    //Role = "user"
                };
                try
                {
                    OperationDetails operationDetails = await UserService.Create(userDto);
                    if (operationDetails.Succedeed)
                    {
                        userDto.Id = operationDetails.Id;
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userDto.Id, operationDetails.Value },
                                   protocol: Request.Url.Scheme);
                        // отправка письма
                        await UserService.SendEmailAsync(userDto.Id, callbackUrl);
                        return View("DisplayEmail");
                    }
                    else
                        ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        Response.Write("Object: " + validationError.Entry.Entity.ToString());
                        Response.Write("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Response.Write(err.ErrorMessage + "");
                        }
                    }
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string Value)
        {
            if (userId == null || Value == null)
            {
                return View("Error");
            }
            var result = await UserService.ConfirmEmailAsync(userId, Value);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }


        public ActionResult DisplayEmail()
        {
            return View();
        }
    }
}