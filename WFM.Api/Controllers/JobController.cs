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
    public async Task<IActionResult> GetAllJobsAsync()
    {
        var jobs  = await jobService.GetAllAsync();
        var result = mapper.Map<IEnumerable<JobUxModel>>(jobs);
        
        try
        {
            if (result is null)
            {
                return NotFound("No jobs found");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetJobByIdAsync(int id)
    {
        var result = await jobService.GetByIdAsync(id);
        var job = mapper.Map<JobUxModel>(result);

        try
        {
            if (job is null)
            {
                return NotFound("No job found with the given id");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize (Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddJobAsync(JobUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var job = mapper.Map<Job>(model);
        var isSuccess = await jobService.AddAsync(job);

        if (isSuccess) return Ok("Job was added successfully");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);

    }
    
    [Authorize (Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditJobAsync(int id, JobUxModel model)
    {
        if (id != model.JobId)
        {
            return BadRequest(ModelState);
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var job = mapper.Map<Job>(model);
        var isSuccess = await jobService.UpdateAsync(job);

        if (isSuccess) return Ok("Success");
        
        ModelState.AddModelError("Error", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }
    
    [Authorize (Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveJobAsync(int id)
    {
        var job = await jobService.GetByIdAsync(id);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            if (job is not null)
            {
                var isDeleted = await jobService.DeleteAsync(job);

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