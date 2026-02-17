using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WFM.UxModels.Models;
using WFM.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WFM.Database.Models;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController(IRoleService roleService, IMapper mapper) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var roles = await roleService.GetAllAsync();
        var result = mapper.Map<IEnumerable<RoleUxModel>>(roles);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(RoleUxModel model)
    {
        var role = mapper.Map<Role>(model);
        var isAdded = await roleService.AddAsync(role);

        return Ok(isAdded);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, RoleUxModel model)
    {
        if (id != model.Id)
        {
            return BadRequest(ModelState);
        }

        var role = mapper.Map<Role>(model);
        var isEdited = await roleService.UpdateAsync(role);

        return Ok(isEdited);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await roleService.DeleteAsync(id);
        
        return Ok(isDeleted);
    }
}