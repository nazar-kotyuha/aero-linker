using AeroLinker.Core.Common.DTO.Auth;
using AeroLinker.Core.Common.DTO.Users;
using AeroLinker.Core.DAL.Entities;

namespace AeroLinker.Core.BLL.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(int id);
    Task<User> GetUserByIdInternalAsync(int id);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByUsernameAsync(string username);
    Task<UserDto> CreateUserAsync(UserRegisterDto userDto, bool isGoogleAuth);
    Task<User?> GetUserEntityByEmailAsync(string email);
    Task<User?> GetUserEntityByUsernameAsync(string username);
    Task<ICollection<UserDto>> GetAllUsersAsync();
    Task<UserProfileDto> GetUserProfileAsync();
    Task<UserProfileDto> UpdateUserNamesAsync(UpdateUserNamesDto updateUserDto);
    Task ChangePasswordAsync(UpdateUserPasswordDto userDto);
}