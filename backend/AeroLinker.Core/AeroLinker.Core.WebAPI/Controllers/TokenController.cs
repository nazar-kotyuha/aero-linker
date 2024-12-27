using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Core.Common.DTO.Auth;

namespace AeroLinker.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public sealed class TokenController : ControllerBase
{
    private readonly IAuthService _authService;

    public TokenController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshedAccessTokenDto>> RefreshTokensAsync(RefreshedAccessTokenDto tokens)
    {
        return Ok(await _authService.RefreshTokensAsync(tokens));
    }
}