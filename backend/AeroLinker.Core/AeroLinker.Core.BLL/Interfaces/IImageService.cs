using Microsoft.AspNetCore.Http;

namespace AeroLinker.Core.BLL.Interfaces;

public interface IImageService
{
    Task AddAvatarAsync(IFormFile avatar);
    Task DeleteAvatarAsync();
}