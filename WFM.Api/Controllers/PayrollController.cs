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
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var payrolls = await payrollService.GetAllAsync();
        var result = mapper.Map<IEnumerable<PayrollUxModel>>(payrolls);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var payroll = await payrollService.GetByIdAsync(id);
        var result = mapper.Map<PayrollUxModel>(payroll);

        return Ok(result);
    }

    [HttpGet("{employeeId:int}")]
    public async Task<IActionResult> GetByEmployeeIdAsync(int employeeId)
    {
        var payroll = await payrollService.GetByEmployeeIdAsync(employeeId);
        var result = mapper.Map<PayrollUxModel>(payroll);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(PayrollUxModel model)
    {
        var payroll = mapper.Map<Payroll>(model);
        var isAdded = await payrollService.AddAsync(payroll);

        return Ok(isAdded);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, PayrollUxModel model)
    {
        if (id != model.Id)
        {
            return BadRequest(ModelState);
        }

        var payroll = mapper.Map<Payroll>(model);
        var isEdited = await payrollService.UpdateAsync(payroll);

        return Ok(isEdited);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await payrollService.DeleteAsync(id);
        
        return Ok(isDeleted);
    }
}