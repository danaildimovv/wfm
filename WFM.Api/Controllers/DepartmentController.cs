using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WFM.Database.Models;
using WFM.UxModels.Models;
using WFM.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]

public class DepartmentController(IDepartmentService departmentService, IMapper mapper) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var departments = await departmentService.GetAllAsync();
        var result = mapper.Map<List<DepartmentUxModel>>(departments);
        
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var department = await departmentService.GetByIdAsync(id);
        var result = mapper.Map<DepartmentUxModel>(department);
            
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(DepartmentUxModel model)
    {
        var department = mapper.Map<Department>(model);
        var isAdded = await departmentService.AddAsync(department);

        return Ok(isAdded);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, DepartmentUxModel model)
    {
        if (id != model.Id)
        {
            return BadRequest(ModelState);
        }
        
        var department = mapper.Map<Department>(model);
        var isUpdated = await departmentService.UpdateAsync(department);

        return Ok(isUpdated);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await departmentService.DeleteAsync(id);
        
        return Ok(isDeleted);
    }
}
