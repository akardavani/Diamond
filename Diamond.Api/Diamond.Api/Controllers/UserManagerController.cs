using Diamond.Api.ViewModels.UserManager;
using Diamond.API.Controllers;
using Diamond.Services.BusinessService;
using Diamond.Services.BusinessServiceDto.UserManagerControllerDtos;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.Api.Controllers
{
    public class UserManagerController : ApiBaseController
    {
        private readonly ManageUserBusinessServices _manageUserBusinessServices;

        public UserManagerController(ManageUserBusinessServices manageUserBusinessServices)
        {
            _manageUserBusinessServices = manageUserBusinessServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _manageUserBusinessServices.GetAllUsers();
            var result = users.Data;
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FindUserById(string id)
        {
            var user =await _manageUserBusinessServices.GetUserById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(model);
            }
            var user = await _manageUserBusinessServices.EditUser(new EditUserDto()
            {
                Id = model.Id,
                UserName = model.UserName,
                SecurityStamp = model.SecurityStamp,
                Email = model.Email,
            });
            var result = user;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _manageUserBusinessServices.DeleteUser(id);
            return Ok(result);
        }

    }
}
