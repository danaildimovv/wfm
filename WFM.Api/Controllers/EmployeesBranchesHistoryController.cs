using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WFM.Api.Services.Interfaces;
using WFM.UxModels.Models;

namespace WFM.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class EmployeesBranchesHistoryController(IEmployeesBranchesHistoryService employeeBranchesService, IMapper mapper) : Controller
{
    [Authorize(Roles = "Admin")]
    [HttpGet("{employeeId:int}")]
    public async Task<IActionResult> GetBranchesByEmployeeIdAsync(int employeeId)
    {
        var branches  = await employeeBranchesService.GetBranchesByEmployeeIdAsync(employeeId);
        
        try
        {
            if (branches.Count == 0)
            {
                return NotFound("No branches found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = mapper.Map<IEnumerable<EmployeesBranchesHistoryUxModel>>(branches);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{branchId:int}")]
    public async Task<IActionResult> GetEmployeesByBranchIdAsync(int branchId)
    {
        var employees  = await employeeBranchesService.GetEmployeesByBranchIdAsync(branchId);
        
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
            
            var result = mapper.Map<IEnumerable<EmployeesBranchesHistoryUxModel>>(employees);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}