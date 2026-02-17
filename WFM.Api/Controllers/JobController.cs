using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using WFM.UxModels.Models;
using WFM.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WFM.Database.Models;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class JobController(IJobService jobService, IMapper mapper) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var jobs = await jobService.GetAllAsync();
        var result = mapper.Map<IEnumerable<JobUxModel>>(jobs);
        
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var job = await jobService.GetByIdAsync(id);
        var result = mapper.Map<JobUxModel>(job);

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(JobUxModel model)
    {
        var job = mapper.Map<Job>(model);
        var isAdded = await jobService.AddAsync(job);

        return Ok(isAdded);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, JobUxModel model)
    {
        if (id != model.Id)
        {
            return BadRequest(ModelState);
        }

        var job = mapper.Map<Job>(model);
        var isEdited = await jobService.UpdateAsync(job);

        return Ok(isEdited);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await jobService.DeleteAsync(id);
        
        return Ok(isDeleted);
    }
}