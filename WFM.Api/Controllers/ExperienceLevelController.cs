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
    public async Task<IActionResult> GetAllAsync()
    {
        var experienceLevels = await experienceLevelService.GetAllAsync();
        var result = mapper.Map<IEnumerable<ExperienceLevelUxModel>>(experienceLevels);
            
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var experienceLevel = await experienceLevelService.GetByIdAsync(id);
        var result = mapper.Map<ExperienceLevel>(experienceLevel);
            
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(ExperienceLevelUxModel model)
    {
        var experienceLevel = mapper.Map<ExperienceLevel>(model);
        var isAdded = await experienceLevelService.AddAsync(experienceLevel);

        return Ok(isAdded);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, ExperienceLevelUxModel model)
    {
        if (id != model.Id)
        {
            return BadRequest(ModelState);
        }

        var experienceLevel = mapper.Map<ExperienceLevel>(model);
        var isUpdated = await experienceLevelService.UpdateAsync(experienceLevel);

        return Ok(isUpdated);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await experienceLevelService.DeleteAsync(id);
        
        return Ok(isDeleted);
    }
}