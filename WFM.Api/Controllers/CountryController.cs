using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.UxModels.Models;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]

public class CountryController(ICountryService countryService, IMapper mapper) : Controller
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var countries = await countryService.GetAllAsync();
        var result = mapper.Map<List<CountryUxModel>>(countries);
        
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var country = await countryService.GetByIdAsync(id);
        var result = mapper.Map<CountryUxModel>(country);
            
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAsync(CountryUxModel model)
    {
        var country = mapper.Map<Country>(model);
        var isAdded = await countryService.AddAsync(country);

        return Ok(isAdded);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditAsync(int id, CountryUxModel model)
    {
        if (id != model.Id)
        {
            return BadRequest(ModelState);
        }

        var country = mapper.Map<Country>(model);
        var isUpdated = await countryService.UpdateAsync(country);

        return Ok(isUpdated);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var isDeleted = await countryService.DeleteAsync(id);
        
        return Ok(isDeleted);
    }
}
