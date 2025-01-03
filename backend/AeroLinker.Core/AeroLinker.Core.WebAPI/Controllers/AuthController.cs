﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Core.Common.DTO.Auth;
using AeroLinker.Core.Common.DTO.Users;

namespace AeroLinker.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Generates user token for login
    /// </summary>
    /// <remarks>
    /// Sample request for login:
    ///
    ///     POST /api/auth/login
    ///     {
    ///        "email": "test@gmail.com",
    ///        "password": "myPassword1"
    ///     }
    ///
    /// </remarks>
    [HttpPost("login")]
    public async Task<ActionResult<AuthUserDto>> LoginAsync([FromBody] UserLoginDto userLoginData)
    {
        return Ok(await _authService.LoginAsync(userLoginData));
    }

    /// <summary>
    /// Registers new user and generates tokens
    /// </summary>
    /// <remarks>
    /// Sample request for registration:
    ///
    ///     POST /api/auth/register
    ///     {
    ///        "email": "test@gmail.com",
    ///        "username": "username",
    ///        "firstName": "name",
    ///        "lastName": "surname",
    ///        "password": "myPassword1"
    ///     }
    ///
    /// </remarks>
    [HttpPost("register")]
    public async Task<ActionResult<AuthUserDto>> RegisterAsync([FromBody] UserRegisterDto userRegisterData)
    {
        return Ok(await _authService.RegisterAsync(userRegisterData));
    }

    /// <summary>
    /// Generates user token for login with google tokenId
    /// </summary>
    /// <remarks>
    /// Sample request for login:
    ///
    ///     POST /api/auth/login/google
    ///     {
    ///        "idToken": "token",
    ///     }
    ///
    /// </remarks>
    [HttpPost("login/google")]
    [ProducesResponseType(typeof(AuthUserDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthUserDto>> LoginWithGoogleAsync([FromBody] GoogleToken googleToken)
    {
        return Ok(await _authService.AuthorizeWithGoogleAsync(googleToken.IdToken));
    }
}