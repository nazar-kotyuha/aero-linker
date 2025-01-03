﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AeroLinker.AzureBlobStorage.Interfaces;
using AeroLinker.Core.BLL.Extensions;
using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Core.BLL.Services.Abstract;
using AeroLinker.Core.Common.DTO.Auth;
using AeroLinker.Core.Common.DTO.Users;
using AeroLinker.Core.Common.Security;
using AeroLinker.Core.DAL.Context;
using AeroLinker.Core.DAL.Entities;
using AeroLinker.Shared.Exceptions;

namespace AeroLinker.Core.BLL.Services;

public sealed class UserService : BaseService, IUserService
{
    private const int MaxNameLength = 25;
    private const int MinNameLength = 2;
    private readonly IUserIdGetter _userIdGetter;
    private readonly IBlobStorageService _blobStorageService;

    public UserService(AeroLinkerCoreContext context, IMapper mapper, IUserIdGetter userIdGetter,
        IBlobStorageService blobStorageService) : base(context, mapper)
    {
        _userIdGetter = userIdGetter;
        _blobStorageService = blobStorageService;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        return _mapper.Map<UserDto>(await GetUserByIdInternalAsync(id));
    }

    public async Task<UserProfileDto> GetUserProfileAsync()
    {
        return _mapper.Map<UserProfileDto>(await GetUserByIdInternalAsync(_userIdGetter.GetCurrentUserId()));
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var userEntity = await GetUserEntityByEmailAsync(email);
        if (userEntity is null)
        {
            throw new EntityNotFoundException(nameof(User), email);
        }

        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<ICollection<UserDto>> GetAllUsersAsync()
    {
        var userEntities = await _context.Users.ToListAsync();

        if (userEntities is null)
        {
            return new List<UserDto>();
        }

        return _mapper.Map<List<UserDto>>(userEntities);
    }

    public async Task<UserDto> GetUserByUsernameAsync(string username)
    {
        var userEntity = await GetUserEntityByUsernameAsync(username);
        if (userEntity is null)
        {
            throw new EntityNotFoundException(nameof(User), username);
        }

        return _mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> CreateUserAsync(UserRegisterDto userDto, bool isGoogleAuth)
    {
        if (await GetUserEntityByUsernameAsync(userDto.Username) is not null)
        {
            if (isGoogleAuth)
            {
                // Google registration must not fail because of the same username.
                userDto.Username = GenerateRandomUsername();
            }
            else
            {
                throw new UsernameAlreadyRegisteredException();
            }
        }

        if (await GetUserEntityByEmailAsync(userDto.Email) is not null)
        {
            throw new EmailAlreadyRegisteredException();
        }

        var newUser = PrepareNewUserData(userDto, isGoogleAuth);
        var createdUser = (await _context.Users.AddAsync(newUser)).Entity;
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task<UserProfileDto> UpdateUserNamesAsync(UpdateUserNamesDto updateUserDto)
    {
        var userEntity = await GetUserByIdInternalAsync(_userIdGetter.GetCurrentUserId());

        var existingUserWithSameUsername = await GetUserEntityByUsernameAsync(updateUserDto.Username);

        if (existingUserWithSameUsername is not null && existingUserWithSameUsername.Id != _userIdGetter.GetCurrentUserId())
        {
            throw new UsernameAlreadyRegisteredException();
        }

        _mapper.Map(updateUserDto, userEntity);

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserProfileDto>(userEntity);
    }

    public async Task ChangePasswordAsync(UpdateUserPasswordDto changePasswordDto)
    {
        var userEntity = await GetUserByIdInternalAsync(_userIdGetter.GetCurrentUserId());

        if (!SecurityUtils.ValidatePassword(changePasswordDto.CurrentPassword, userEntity.PasswordHash!,
                userEntity.Salt!))
        {
            throw new InvalidPasswordException();
        }

        userEntity.PasswordHash = SecurityUtils.HashPassword(changePasswordDto.NewPassword, userEntity.Salt!);

        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserEntityByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserEntityByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> GetUserByIdInternalAsync(int id)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userEntity is null)
        {
            throw new EntityNotFoundException(nameof(User), id);
        }

        return userEntity;
    }

    private string GenerateRandomUsername()
        => ("user" + Guid.NewGuid()).Truncate(MaxNameLength);

    private void AdaptUserNames(UserRegisterDto user)
    {
        user.FirstName = user.FirstName.PadRight(MinNameLength, '-').Truncate(MaxNameLength);
        user.LastName = user.LastName.PadRight(MinNameLength, '-').Truncate(MaxNameLength);
    }

    private void HashUserPassword(User newUser, string password)
    {
        var salt = SecurityUtils.GenerateRandomSalt();
        newUser.Salt = salt;
        newUser.PasswordHash = SecurityUtils.HashPassword(password, salt);
    }

    private User PrepareNewUserData(UserRegisterDto userDto, bool isGoogleAuth)
    {
        // User's first name and last name from Google account might be too long or too short,
        // so we need to adapt it to meet our requirements.
        AdaptUserNames(userDto);
        var newUser = _mapper.Map<User>(userDto)!;
        newUser.IsGoogleAuth = isGoogleAuth;
        if (!isGoogleAuth)
        {
            HashUserPassword(newUser, userDto.Password);
        }

        return newUser;
    }
}