using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WFM.Database.Models;
using WFM.UxModels.Models;
using WFM.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BranchController(IBranchService branchService, IMapper mapper) : Controller
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var branch = await branchService.GetAllAsync();
        var result = mapper.Map<IEnumerable<BranchUxModel>>(branch);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var branch = await branchService.GetByIdAsync(id);
        var result = mapper.Map<BranchUxModel>(branch);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(BranchUxModel model)
    {
        var branch = mapper.Map<Branch>(model);
        var isAdded = await branchService.AddAsync(branch);
            
        return Ok(isAdded);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, BranchUxModel model)
    {
        if (id != model.Id)
        {
            return BadRequest(ModelState);
        }

        var branch = mapper.Map<Branch>(model);
        var isUpdated = await branchService.UpdateAsync(branch);

        return Ok(isUpdated);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await branchService.DeleteAsync(id);

        return Ok(isDeleted);
    }
}