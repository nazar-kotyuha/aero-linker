using AeroLinker.Core.Common.DTO.Auth;
using AeroLinker.Core.Common.DTO.Users;

namespace AeroLinker.Core.BLL.Interfaces;

public interface IAuthService
{
    Task<AuthUserDto> LoginAsync(UserLoginDto userLoginDto);
    Task<AuthUserDto> RegisterAsync(UserRegisterDto userRegisterDto);
    Task<AuthUserDto> AuthorizeWithGoogleAsync(string googleCredentialsToken);
    Task<RefreshedAccessTokenDto> RefreshTokensAsync(RefreshedAccessTokenDto tokens);
}