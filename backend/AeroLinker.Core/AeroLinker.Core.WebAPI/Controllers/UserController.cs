﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Core.Common.DTO.Users;

namespace AeroLinker.Core.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserIdGetter _userIdGetter;
    private readonly IUserService _userService;
    private readonly IImageService _imageService;

    public UserController(IUserIdGetter userIdGetter, IUserService userService, IImageService imageService)
    {
        _userIdGetter = userIdGetter;
        _userService = userService;
        _imageService = imageService;
    }

    /// <summary>
    /// Update user names
    /// </summary>
    [HttpPut("update-names")]
    public async Task<ActionResult<UserProfileDto>> UpdateUserNamesAsync([FromBody] UpdateUserNamesDto updateUserDto)
    {
        return Ok(await _userService.UpdateUserNamesAsync(updateUserDto));
    }

    /// <summary>
    /// Update user password
    /// </summary>
    [HttpPut("update-password")]
    public async Task<ActionResult> UpdatePasswordAsync([FromBody] UpdateUserPasswordDto changePasswordDto)
    {
        await _userService.ChangePasswordAsync(changePasswordDto);
        return NoContent();
    }

    [HttpGet("fromToken")]
    public async Task<ActionResult<UserDto>> GetUserFromTokenAsync()
    {
        return Ok(await _userService.GetUserByIdAsync(_userIdGetter.GetCurrentUserId()));
    }

    [HttpGet("user-profile")]
    public async Task<ActionResult<UserProfileDto>> GetUserProfileAsync()
    {
        return Ok(await _userService.GetUserProfileAsync());
    }

    [HttpPost("add-avatar")]
    public async Task<ActionResult> AddUserAvatarAsync(IFormFile avatar)
    {
        await _imageService.AddAvatarAsync(avatar);
        return NoContent();
    }
    
    [HttpDelete("delete-avatar")]
    public async Task<ActionResult> DeleteUserAvatarAsync()
    {
        await _imageService.DeleteAvatarAsync();
        return NoContent();
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<ICollection<UserDto>>> GetAllUsersAsync()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }
}
