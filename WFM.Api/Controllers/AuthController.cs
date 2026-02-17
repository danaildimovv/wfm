using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WFM.Api.Services.Interfaces;
using WFM.UxModels.Models;

namespace WFM.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]

public class AuthController(IAuthService authService, IMapper mapper) : Controller
{
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
    public async Task<IActionResult> RegisterAsync(UserUxModel user)
    {
        var isRegistered = await authService.RegisterAsync(user);

        return Ok(isRegistered);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
    public async Task<IActionResult> LoginAsync(UserUxModel model)
    {
        var token = await authService.LoginAsync(model);

        return Json(token);
    }
}