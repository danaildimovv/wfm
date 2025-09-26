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
    public async Task<IActionResult> GetAllVacationsAsync()
    {
        var vacations  = await vacationService.GetAllAsync();
        
        try
        {
            if (!vacations.Any())
            {
                return NotFound("No vacations found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<IEnumerable<PayrollUxModel>>(vacations);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{employeeId:long}")]
    public async Task<IActionResult> GetVacationsByEmployeeIdAsync(long employeeId)
    {
        var vacations  = await vacationService.GetVacationsByEmployeeIdAsync(employeeId);
        
        try
        {
            if (!vacations.Any())
            {
                return NotFound("No vacations found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<IEnumerable<PayrollUxModel>>(vacations);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddVacationRequestAsync(VacationUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vacation = mapper.Map<Vacation>(model);
        var isSuccess = await vacationService.AddAsync(vacation);

        if (isSuccess) return Ok("Vacation was added successfully");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);

    }
    
    [Authorize (Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UpdateVacationRequestAsync(VacationUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vacation = mapper.Map<Vacation>(model);
        
        var isSuccess = await vacationService.UpdateAsync(vacation);

        if (isSuccess) return Ok("Vacation was updated successfully");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);

    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteVacationRequestAsync(int vacationId)
    {
        var vacation = await vacationService.GetByIdAsync(vacationId);
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            if (vacation is not null)
            {
                var isSuccess = await vacationService.DeleteAsync(vacation);

                if (isSuccess) return Ok("Vacation was deleted successfully");
            }

            ModelState.AddModelError("Error", "Something Went Wrong");
            return StatusCode(500, ModelState);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}