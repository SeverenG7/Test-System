using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
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
        #region Init services
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

        #endregion

        #region Login/Logout & Forgot Password
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserDto userDto = new UserDto { Email = model.Email, Password = model.Password };
                    OperationDetails details = await UserService.AuthenticateAsync(userDto);
                    ClaimsIdentity claim = details.Value;
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

                        if (details.Message.Equals("user"))
                        {
                            return RedirectToAction("MainMenu" , "User");
                        }

                        if (details.Message.Equals("admin"))
                        {
                            return RedirectToAction("CommonTables", "Common");
                        }
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

        [AllowAnonymous]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                OperationDetails operationDetails = await UserService.ForgotPasswordAsync(model.Email);
                if (operationDetails.Succedeed)
                {
                    var callbackUrl = Url.Action("ResetPassword", "Account",
                     new { userId = operationDetails.Id, operationDetails.Value }, protocol: Request.Url.Scheme);
                    string reference =
                        "Для сброса пароля, перейдите по ссылке <a href=\"" + callbackUrl + "\">сбросить</a>";
                    string theme = "Сброс пароля";
                    await UserService.SendEmailAsync(operationDetails.Id, theme, reference);
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");
                }
                else
                {
                    Response.Write("Such e-mail not exist in system");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string Value)
        {
            TempData["token"] = Value;
            return Value == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model )
        {
            string token = TempData["token"] as string;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            OperationDetails details = await UserService.ResetPassworAsync(model.Email, token, model.Password);
            if (details.Succedeed)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        #endregion

        #region Register & E-mail confirm
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
             await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto
                {
                    Email = model.Email,
                    Password = model.Password,
                    UserFirstName = "name",
                    UserLastName = "lastname",
                    Role = "user"
                };
                try
                {
                    OperationDetails operationDetails = await UserService.CreateAsync(userDto);
                    if (operationDetails.Succedeed)
                    {
                        userDto.Id = operationDetails.Id;
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userDto.Id, operationDetails.Value },
                                   protocol: Request.Url.Scheme);
                        string reference = 
                               "Для завершения регистрации перейдите по ссылке:: <a href=\""
                                                               + callbackUrl + "\">завершить регистрацию</a>";
                        string theme = "Подтверждение электронной почты";
                        await UserService.SendEmailAsync(userDto.Id, theme , reference );
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

        [HttpGet]
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult DisplayEmail()
        {
            return View();
        }

        #endregion
        
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialDataAsync( new List<string> { "user", "admin" });
        }

    }
}