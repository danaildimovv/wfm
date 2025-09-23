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
    public async Task<IActionResult> GetAllCountriesAsync()
    {
        var countries = await countryService.GetAllAsync();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!countries.Any())
        {
            return NotFound("No countries");
        }

        var result = mapper.Map<List<CountryUxModel>>(countries);
        
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCountryByIdAsync(int id)
    {
        var country = await countryService.GetByIdAsync(id);

        try
        {
            if (country is null)
            {
                return NotFound("No country with the given id");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = mapper.Map<CountryUxModel>(country);
            
            return Ok(result);
        }

        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCountryAsync(CountryUxModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var country = mapper.Map<Country>(model);
        var isSuccessfullyAdded = await countryService.AddAsync(country);

        if (isSuccessfullyAdded) return Ok("Success");

        ModelState.AddModelError("", "Something Went Wrong");
        return StatusCode(500, ModelState);

    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditCountryAsync(int id, CountryUxModel model)
    {
        if (id != model.CountryId)
        {
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var country = mapper.Map<Country>(model);
        var isUpdated = await countryService.UpdateAsync(country);

        if (isUpdated) return Ok("Success");

        ModelState.AddModelError("", "Something Went Wrong");
        return StatusCode(500, ModelState);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCountryAsync(int id)
    {
        var country = await countryService.GetByIdAsync(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            if (country is not null)
            {
                var isSuccessfullyDeleted = await countryService.DeleteAsync(country);

                if (isSuccessfullyDeleted) return Ok("Success");
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
