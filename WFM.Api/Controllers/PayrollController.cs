using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WFM.Database.Models;

using Microsoft.AspNetCore.Mvc;
using WFM.Api.Services.Interfaces;
using WFM.UxModels.Models;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]

public class PayrollController(IPayrollService payrollService, IMapper mapper) : Controller
{
    [Authorize (Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllPayrollsAsync()
    {
        var payrolls  = await payrollService.GetAllAsync();
        
        try
        {
            if (!payrolls.Any())
            {
                return NotFound("No payrolls found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<IEnumerable<PayrollUxModel>>(payrolls);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize (Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPayrollByIdAsync(int id)
    {
        var payroll = await payrollService.GetByIdAsync(id);

        try
        {
            if (payroll is null)
            {
                return NotFound("No payroll found with the given id");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<PayrollUxModel>(payroll);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize (Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddPayrollAsync(PayrollUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var payroll = mapper.Map<Payroll>(model);
        var isSuccess = await payrollService.AddAsync(payroll);

        if (isSuccess) return Ok("Payroll item was added successfully");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);

    }
    
    [Authorize (Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditPayrollAsync(int id, PayrollUxModel model)
    {
        if (id != model.PayrollId)
        {
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var payroll = mapper.Map<Payroll>(model);
        var isSuccess = await payrollService.UpdateAsync(payroll);

        if (isSuccess) return Ok("Success");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }
    
    [Authorize (Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemovePayrollItemAsync(int id)
    {
        var payrollItem = await payrollService.GetByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            if (payrollItem is not null)
            {
                var isDeleted = await payrollService.DeleteAsync(payrollItem);

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