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
    public async Task<IActionResult> GetAllRolesAsync()
    {
        var roles  = await roleService.GetAllAsync();
        var result = mapper.Map<IEnumerable<RoleUxModel>>(roles);
        
        try
        {
            if (result is null)
            {
                return NotFound("No roles found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize (Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddRoleAsync(RoleUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var role = mapper.Map<Role>(model);
        var isSuccess = await roleService.AddAsync(role);

        if (isSuccess) return Ok("Role was added successfully");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);

    }
    
    [Authorize (Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditRoleAsync(int id, RoleUxModel model)
    {
        if (id != model.RoleId)
        {
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var role = mapper.Map<Role>(model);
        var isSuccess = await roleService.UpdateAsync(role);

        if (isSuccess) return Ok("Success");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }
    
    [Authorize (Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveRoleAsync(int id)
    {
        var role = await roleService.GetByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            if (role is not null)
            {
                var isDeleted = await roleService.DeleteAsync(role);

                if (isDeleted) return Ok("Successfully deleted");
            }
            
            ModelState.AddModelError("Error", "Something Went Wrong");
            return StatusCode(500, ModelState);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}