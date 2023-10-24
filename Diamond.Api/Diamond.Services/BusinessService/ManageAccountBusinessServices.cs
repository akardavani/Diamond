using Diamond.Domain.Models.Identity;
using Diamond.MessageManager;
using Diamond.Services.BusinessServiceDto.AccountControllerDtos;
using Diamond.Utils.ApiResult;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class ManageAccountBusinessServices : IBusinessService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public ManageAccountBusinessServices(UserManager<User> userManager,SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        #region [Login]
        public async Task<ApiResult> Login(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, dto.RememberMe, true);
            if (result.IsLockedOut)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    Error = "Your Account Has been banned."
                };
            }
            return new ApiResult()
            {
                IsSuccess = result.Succeeded,
                Error = result.Succeeded == true ? "Login Succeeded." : "UserName Or Password Is Wrong.",
            };
        }
        #endregion

        #region [LogOut]
        public async Task<ApiResult> LogOut()
        {
            //try
            //{
                await _signInManager.SignOutAsync();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("", ex);
            //}
            return new ApiResult()
            {
                IsSuccess = true,
                Error = "SignOut Succeeded."
            };
        }
        #endregion

        #region [CreateUser]
        public async Task<ApiResult> RegisterUser(RegisterUserDto dto)
        {
            User user = new()
            {
                Email = dto.Email,
                UserName = dto.UserName,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            emailConfirmationToken.Replace(emailConfirmationToken, "Token");
            if (result.Succeeded)
            {
                await _emailSender.SendAsync(dto.Email, "", dto.Url);
            }
            else
            {
                return new ApiResult
                {
                    IsSuccess = false,
                    Error = result.Errors.FirstOrDefault()?.Description,
                };
            }
            return new ApiResult
            {
                IsSuccess = true,
            };
        }
        #endregion

        #region [ForgotPassword]
        public async Task<ApiResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    Error = "User Not Found."
                };
            }
            var emailPasswordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            emailPasswordResetToken.Replace(emailPasswordResetToken, "Token");
            return new ApiResult()
            {
                IsSuccess = true,
                Error = ""
            };
        }
        #endregion

        #region [ConfirmEmail]
        public async Task<ApiResult> ConfirmEmail(string userName, string token)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
                return new ApiResult()
                {
                    IsSuccess = false,
                    Error = "Cannot Find User."
                };
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    Error = "Cannot Find User."
                };
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return new ApiResult()
            {
                IsSuccess = result.Succeeded,
                Error = result.Succeeded == true ? "Email Confirmed" : "Email Not Confirmed"
            };
        }
        #endregion
    }
}
