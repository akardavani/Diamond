using Diamond.Domain.Models.Identity;
using Diamond.Services.BusinessServiceDto.RoleManagerControllerDtos;
using Diamond.Utils.ApiResult;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class ManageRoleBusinessServices : IBusinessService
    {
        private readonly RoleManager<Role> _roleManager;

        public ManageRoleBusinessServices(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        #region [GetAllRoles]
        public async Task<ApiResult<List<Role>>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return new ApiResult<List<Role>>() { Data = roles };

        }
        #endregion

        #region [AddRole]
        public async Task<ApiResult> Add(AddRoleDto dto)
        {
            var response = ValidationAdd(dto);
            if (response.IsSuccess)
            {
                return response;
            }

            var result = await _roleManager.CreateAsync(new()
            {
                Name = dto.Name,
            });

            return new ApiResult()
            {
                IsSuccess = result.Succeeded,
                Error = result.Errors.FirstOrDefault()?.Description
            };
        }

        private static ApiResult ValidationAdd(AddRoleDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    Error = "Please Enter a Valid Role Name."
                };
            }

            return new ApiResult { IsSuccess = true };
        }
        #endregion

        #region [FindRoleById]
        public async Task<ApiResult<Role>> GetRoleById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new ApiResult<Role>()
                {
                    IsSuccess = false,
                    Error = "Role Was Not Found."
                };
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return new ApiResult<Role>()
                {
                    IsSuccess = false,
                    Error = "Role Was Not Found."
                };
            }
            return new ApiResult<Role>() { Data = role };
        }
        #endregion

        #region [EditRole]
        public async Task<ApiResult> Edit(EditRoleDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id);
            if (role == null)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    Error = "Role Was Not Found."
                };
            }
            role.Name = dto.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            return new ApiResult()
            {
                IsSuccess = result.Succeeded,
                Error = result.Errors.FirstOrDefault()?.Description
            };
        }
        #endregion

        #region [DeleteRole]
        public async Task<ApiResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return new ApiResult()
                {
                    IsSuccess = false,
                    Error = "The Role Was Not Found."
                };
            }
            var result = await _roleManager.DeleteAsync(role);
            return new ApiResult()
            {
                IsSuccess = result.Succeeded,
                Error = result.Errors.FirstOrDefault()?.Description
            };
        }
        #endregion
    }
}
