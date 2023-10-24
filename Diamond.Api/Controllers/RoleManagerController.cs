using Diamond.Api.ViewModels.RoleManager;
using Diamond.API.Controllers;
using Diamond.Services.BusinessService;
using Diamond.Services.BusinessServiceDto.RoleManagerControllerDtos;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.Api.Controllers
{
    public class RoleManagerController : ApiBaseController
    {
        private readonly ManageRoleBusinessServices _manageRoleBusinessServices;

        public RoleManagerController(ManageRoleBusinessServices manageRoleBusinessServices)
        {
            _manageRoleBusinessServices = manageRoleBusinessServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _manageRoleBusinessServices.GetAll();
            var result = roles.Data;
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> FindRoleById(string id)
        {
            var role = await _manageRoleBusinessServices.GetRoleById(id);
            return Ok(role);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newRole = await _manageRoleBusinessServices.Add(new AddRoleDto()
                {
                    Name = model.Name,
                });
                return Ok(newRole);
            }
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _manageRoleBusinessServices.Edit(new EditRoleDto()
                {
                    Id = model.Id,
                    RoleName = model.Name
                });
                return Ok(role);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _manageRoleBusinessServices.Delete(id);
            return Ok(result);
        }
    }
}
