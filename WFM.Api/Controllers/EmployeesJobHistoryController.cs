using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WFM.Api.Services.Interfaces;
using WFM.UxModels.Models;

namespace WFM.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]

public class EmployeesJobHistoryController(IEmployeesJobHistoryService employeesJobHistoryService, IMapper mapper) : Controller
{
    [Authorize(Roles = "Admin")]
    [HttpGet("{employeeId:int}")]
    public async Task<IActionResult> GetJobsByEmployeeIdAsync(int employeeId)
    {
        var jobs  = await employeesJobHistoryService.GetJobsByEmployeeIdAsync(employeeId);
        
        try
        {
            if (jobs.Count == 0)
            {
                return NotFound("No job found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = mapper.Map<IEnumerable<EmployeesJobHistoryUxModel>>(jobs);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{jobId:int}")]
    public async Task<IActionResult> GetEmployeesByJobIdAsync(int jobId)
    {
        var employees  = await employeesJobHistoryService.GetEmployeesByJobIdAsync(jobId);
        
        try
        {
            if (employees.Count == 0)
            {
                return NotFound("No employees found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = mapper.Map<IEnumerable<EmployeesJobHistoryUxModel>>(employees);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}