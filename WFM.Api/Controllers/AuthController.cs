using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WFM.Api.Services.Interfaces;
using WFM.Database.Models;
using WFM.UxModels.Models;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]

public class AuthController(IAuthService authService, IMapper mapper) : Controller
{
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
    public async Task<IActionResult> Register(UserUxModel user)
    {
        var isRegistered = await authService.RegisterAsync(user);

        if (!isRegistered)
        {
            return BadRequest("Username already exists");
        }

        return Ok(isRegistered);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
    public async Task<IActionResult> Login(UserUxModel model)
    {
        var token = await authService.LoginAsync(model);

        if (token is null)
        {
            return BadRequest("Invalid username or password");
        }

        return Ok(token);
    }
}