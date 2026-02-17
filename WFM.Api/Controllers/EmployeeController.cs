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
    public async Task<IActionResult> GetAllAsync()
    {
        var employees  = await employeeService.GetAllAsync();
        var result = mapper.Map<IEnumerable<EmployeeUxModel>>(employees);
            
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var employee = await employeeService.GetByIdAsync(id);
        var result = mapper.Map<EmployeeUxModel>(employee);

        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(EmployeeUxModel model)
    {
        var employee = mapper.Map<Employee>(model);
        var isAdded = await employeeService.AddEmployeeAsync(employee);

        return Ok(isAdded);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, EmployeeUxModel model)
    {
        if (id != model.Id)
        {
            return BadRequest(ModelState);
        }

        var employee = mapper.Map<Employee>(model);
        var isEdited = await employeeService.UpdateEmployeeAsync(employee);

        return Ok(isEdited);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await employeeService.DeleteAsync(id);
        
        return Ok(isDeleted);
    }
}