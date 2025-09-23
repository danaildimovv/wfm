using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WFM.Database.Models;
using Microsoft.AspNetCore.Mvc;
using WFM.Api.Services.Interfaces;
using WFM.UxModels.Models;

namespace WFM.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]

public class EmployeeController(IEmployeeService employeeService, IMapper mapper) : Controller
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync()
    {
        var employees  = await employeeService.GetAllAsync();
        
        try
        {
            if (!employees.Any())
            {
                return NotFound("No employees found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = mapper.Map<IEnumerable<EmployeeUxModel>>(employees);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployeeByIdAsync(int id)
    {
        var employee = await employeeService.GetByIdAsync(id);

        try
        {
            if (employee is null)
            {
                return NotFound("No employee found with the given id");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = mapper.Map<EmployeeUxModel>(employee);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddEmployeeAsync(EmployeeUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = mapper.Map<Employee>(model);
        var isSuccess = await employeeService.AddAsync(employee);

        if (isSuccess) return Ok("Employee was added successfully");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);

    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditEmployeeAsync(int id, EmployeeUxModel model)
    {
        if (id != model.EmployeeId)
        {
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = mapper.Map<Employee>(model);
        var isSuccess = await employeeService.UpdateAsync(employee);

        if (isSuccess) return Ok("Successfully saved.");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveEmployeeAsync(int id)
    {
        var employee = await employeeService.GetByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            if (employee is not null)
            {
                var isDeleted = await employeeService.DeleteAsync(employee);

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