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
        var result = mapper.Map<IEnumerable<EmployeesBranchesHistoryUxModel>>(branches);
            
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{branchId:int}")]
    public async Task<IActionResult> GetEmployeesByBranchIdAsync(int branchId)
    {
        var employees = await employeeBranchesService.GetEmployeesByBranchIdAsync(branchId);
        var result = mapper.Map<IEnumerable<EmployeesBranchesHistoryUxModel>>(employees);

        return Ok(result);
    }
}