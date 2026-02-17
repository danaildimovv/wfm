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
        var result = mapper.Map<IEnumerable<EmployeesJobHistoryUxModel>>(jobs);
            
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{jobId:int}")]
    public async Task<IActionResult> GetEmployeesByJobIdAsync(int jobId)
    {
        var employees  = await employeesJobHistoryService.GetEmployeesByJobIdAsync(jobId);
        var result = mapper.Map<IEnumerable<EmployeesJobHistoryUxModel>>(employees);
            
        return Ok(result);
    }
}