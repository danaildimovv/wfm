using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WFM.Database.Models;
using WFM.UxModels.Models;
using WFM.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WFM.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]

public class ExperienceLevelController(IExperienceLevelService experienceLevelService, IMapper mapper) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllExperienceLevelsAsync()
    {
        var experienceLevels = await experienceLevelService.GetAllAsync();
        
        try
        {
            if (!experienceLevels.Any())
            {
                return NotFound("No experience levels found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<IEnumerable<ExperienceLevelUxModel>>(experienceLevels);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetExperienceLevelByIdAsync(int id)
    {
        var experienceLevel = await experienceLevelService.GetByIdAsync(id);

        try
        {
            if (experienceLevel is null)
            {
                return NotFound("No experience level found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<ExperienceLevel>(experienceLevel);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddExperienceLevelAsync(ExperienceLevelUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var experienceLevel = mapper.Map<ExperienceLevel>(model);
        var isSuccess = await experienceLevelService.AddAsync(experienceLevel);

        if (isSuccess)
        {
            return Ok("Success in creating");
        }

        ModelState.AddModelError("", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditExperienceLevelAsync(int id, ExperienceLevelUxModel model)
    {
        if (id != model.ExperienceLevelId)
        {
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var experienceLevel = mapper.Map<ExperienceLevel>(model);
        var isUpdated = await experienceLevelService.UpdateAsync(experienceLevel);

        if (isUpdated) return Ok("Updated");
        
        ModelState.AddModelError("", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteExperienceLevelAsync(int id)
    {
        var experienceLevel = await experienceLevelService.GetByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            if (experienceLevel is not null)
            {
                var isDeleted = await experienceLevelService.DeleteAsync(experienceLevel);

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