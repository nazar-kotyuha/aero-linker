using Microsoft.AspNetCore.Mvc;

namespace AeroLinker.Core.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaticFilesController : Controller
{
    private const string SetupFilePathSection = "SetupFilePath";
    private const string OctetStreamMimeTypeName = "application/octet-stream";
    private const string SetupFileName = "AeroLinkerSetup.pdf";
    private readonly IConfiguration _configuration;

    public StaticFilesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("aerolinker-manual"), DisableRequestSizeLimit]
    public async Task<IActionResult> DownloadAeroLinkerManualAsync()
    {
        var filePath = $"{_configuration[SetupFilePathSection]}/manual-ua.pdf";

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        return File(
            await System.IO.File.ReadAllBytesAsync(filePath),
            OctetStreamMimeTypeName,
            SetupFileName);
    }
}
