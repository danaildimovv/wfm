using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.UxModels.Models;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class VacationController(IVacationService vacationService, IMapper mapper) : Controller
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var vacations = await vacationService.GetAllAsync();
        var result = mapper.Map<IEnumerable<PayrollUxModel>>(vacations);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{employeeId:long}")]
    public async Task<IActionResult> GetVacationsByEmployeeIdAsync(long employeeId)
    {
        var vacations = await vacationService.GetVacationsByEmployeeIdAsync(employeeId);
        var result = mapper.Map<IEnumerable<PayrollUxModel>>(vacations);

        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddAsync(VacationUxModel model)
    {
        var vacation = mapper.Map<Vacation>(model);
        var isAdded = await vacationService.AddAsync(vacation);

        return Ok(isAdded);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> EditAsync(VacationUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vacation = mapper.Map<Vacation>(model);
        var isEdited = await vacationService.UpdateAsync(vacation);

        return Ok(isEdited);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await vacationService.DeleteAsync(id);
        
        return Ok(isDeleted);
    }
}