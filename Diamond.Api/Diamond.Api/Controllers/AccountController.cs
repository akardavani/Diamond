using Diamond.Api.ViewModels.Account;
using Diamond.API.Controllers;
using Diamond.MessageManager;
using Diamond.Services.BusinessService;
using Diamond.Services.BusinessServiceDto.AccountControllerDtos;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.Api.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly IEmailSender _emailSender;
        private readonly ManageAccountBusinessServices _manageAccountBusinessService;

        public AccountController(IEmailSender emailSender,ManageAccountBusinessServices manageAccountBusinessService)
        {
            _emailSender = emailSender;
            _manageAccountBusinessService = manageAccountBusinessService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _manageAccountBusinessService.RegisterUser(new RegisterUserDto()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Url = Url.Action("ConfirmEmail", "Account", new
                    {
                        username = model.UserName,
                        token = "Token",
                    }, protocol: Request.Scheme)

                });
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _manageAccountBusinessService.Login(new LoginDto()
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    RememberMe = model.RememberMe
                });
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _manageAccountBusinessService.LogOut();
            return Ok();
        }

        /*Shoould Be Done:
        [HttpGet]
        public async Task<IActionResult> IsUserNameExist(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> IsEmailExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();
            return Ok(user);
        }*/

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userName, string token)
        {
            var result = await _manageAccountBusinessService.ConfirmEmail(userName, token);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email, string url)
        {
            if (!ModelState.IsValid)
            {
                return Ok(email);
            }
            await _manageAccountBusinessService.ForgotPassword(new ForgotPasswordDto()
            {
                Email = email,
                Url = url
            });
            var emailMessage = Url.Action("ResetPassword", "Account", new
            {
                email = email,
                token = "Token",
            }, protocol: Request.Scheme);
            await _emailSender.SendAsync(email, "", emailMessage);
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }
        
        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return Ok();
        }
    }
}
