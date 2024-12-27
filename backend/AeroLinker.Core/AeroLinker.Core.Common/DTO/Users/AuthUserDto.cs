using AeroLinker.Core.Common.DTO.Auth;

namespace AeroLinker.Core.Common.DTO.Users;

public sealed class AuthUserDto
{
    public UserDto User { get; set; } = null!;
    public RefreshedAccessTokenDto Token { get; set; } = null!;
}
