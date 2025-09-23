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
    public async Task<IActionResult> GetAllDepartmentsAsync()
    {
        var departments = await departmentService.GetAllAsync();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!departments.Any())
        {
            return NotFound("No countries");
        }
        
        var result = mapper.Map<List<DepartmentUxModel>>(departments);
        
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetDepartmentByIdAsync(int id)
    {
        var department = await departmentService.GetByIdAsync(id);

        try
        {
            if (department is null)
            {
                return NotFound("No department with the given id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = mapper.Map<DepartmentUxModel>(department);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddDepartmentAsync(DepartmentUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var department = mapper.Map<Department>(model);
        var isSuccessfullyAdded = await departmentService.AddAsync(department);

        if (isSuccessfullyAdded) return Ok("Successfully saved.");

        ModelState.AddModelError("", "Something Went Wrong");
        return StatusCode(500, ModelState);

    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditDepartmentAsync(int id, DepartmentUxModel model)
    {
        if (id != model.DepartmentId)
        {
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var department = mapper.Map<Department>(model);
        var isUpdated = await departmentService.UpdateAsync(department);

        if (isUpdated) return Ok("Success");

        ModelState.AddModelError("", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteDepartmentAsync(int id)
    {
        var department = await departmentService.GetByIdAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            if (department is not null)
            {
                var isSuccessfullyDeleted = await departmentService.DeleteAsync(department);

                if (isSuccessfullyDeleted) return Ok("Successfully deleted.");
            }
            
            ModelState.AddModelError("", "Error");
            return StatusCode(500, ModelState);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
