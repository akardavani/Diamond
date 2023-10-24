using Diamond.Domain.Models.Identity;
using Diamond.Services.BusinessServiceDto.UserManagerControllerDtos;
using Diamond.Utils.ApiResult;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class ManageUserBusinessServices : IBusinessService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public ManageUserBusinessServices(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region [GetAllUsers]
        public async Task<ApiResult<List<GetAllUsersDto>>> GetAllUsers()
        {
            var userList = await _userManager.Users.Select(u => new GetAllUsersDto()
            {
                Email = u.Email,
                Id = u.Id,
                UserName = u.UserName
            }).ToListAsync();
            return new ApiResult<List<GetAllUsersDto>>()
            {
                Data = userList,
                IsSuccess = true
            };
        }
        #endregion

        #region [FindUserById]
        public async Task<ApiResult<User>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ApiResult<User>()
                {
                    IsSuccess = false,
                    Error = "User Was Not Found."
                };
            }
            return new ApiResult<User>()
            {
                Data = user,
                IsSuccess = true
            };
        }
        #endregion

        #region [EditUser]
        public async Task<ApiResult<User>> EditUser(EditUserDto dto)
        {
            if (string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.UserName))
            {
                return new ApiResult<User>()
                {
                    IsSuccess = false,
                    Error = "User Was Not Found."
                };
            }
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null)
            {
                return new ApiResult<User>()
                {
                    IsSuccess = false,
                    Error = "User Was Not Found."
                };
            }
            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.SecurityStamp = dto.SecurityStamp;
            var result = await _userManager.UpdateAsync(user);
            return new ApiResult<User>()
            {
                IsSuccess = result.Succeeded,
                Error = result.Errors.FirstOrDefault()?.Description,
                Data = user
            };
        }
        #endregion

        #region [DeleteUser]
        public async Task<ApiResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    Error = "User Was Not Found.",
                };
            }
            var result = await _userManager.DeleteAsync(user);
            return new ApiResult()
            {
                IsSuccess = result.Succeeded,
                Error = result.Errors.FirstOrDefault()?.Description,
            };
        }
        #endregion
    }
}
