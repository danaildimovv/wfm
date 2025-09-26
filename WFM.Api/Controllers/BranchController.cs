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
    public async Task<IActionResult> GetAllBranchesAsync()
    {
        var branch = await branchService.GetAllAsync();
        
        try
        {
            if (!branch.Any())
            {
                return NotFound("No branches found.");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = mapper.Map<IEnumerable<BranchUxModel>>(branch);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBranchByIdAsync(int id)
    {
        var branch = await branchService.GetByIdAsync(id);

        try
        {
            if (branch is null)
            {
                return NotFound("No branch found with the given id..");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<BranchUxModel>(branch);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddBranchAsync(BranchUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var branch = mapper.Map<Branch>(model);
        var isSuccess = await branchService.AddAsync(branch);

        if (isSuccess)
        {
            return Ok("Successfully added.");
        }

        ModelState.AddModelError("", "Something Went Wrong");
        return StatusCode(500, ModelState);
        
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditBranchAsync(int id, BranchUxModel model)
    {
        if (id != model.BranchId)
        {
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var branch = mapper.Map<Branch>(model);
        var isUpdated = await branchService.UpdateAsync(branch);

        if (isUpdated) return Ok("Updated");
        
        ModelState.AddModelError("", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBranchAsync(int id)
    {
        var branch = await branchService.GetByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            if (branch is not null)
            {
                var isDeleted = await branchService.DeleteAsync(branch);

                if (isDeleted) return Ok("Success");
            }
            
            ModelState.AddModelError("", "Error");
            return StatusCode(500, ModelState);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}