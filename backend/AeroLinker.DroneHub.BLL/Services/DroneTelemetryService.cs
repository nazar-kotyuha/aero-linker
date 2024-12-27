using AeroLinker.DroneHub.BLL.Interfaces;
using AeroLinker.Shared.DTO.Drone;

namespace AeroLinker.DroneHub.BLL.Services;
public sealed class DroneTelemetryService : IDroneTelemetryService
{
    private readonly DroneTelemetryDto droneTelemetry_;

    public DroneTelemetryService()
    {
        this.droneTelemetry_ = new DroneTelemetryDto
        {
            altitude = 0,
            batteryLevel = 100,
            latitude = 50.6198, // Initial latitude
            longitude = 26.2514, // Initial longitude
            speed = 0,
            status = "on ground",
            timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
    }

    public async Task<DroneTelemetryDto> UpdateDroneTelemetryAsync(CommandDto commandDto)
    {
        switch (commandDto.Option)
        {
            case Option.Up:
                this.droneTelemetry_.latitude += commandDto.Step;
                break;
            case Option.Down:
                this.droneTelemetry_.latitude -= commandDto.Step;
                break;
            case Option.Left:
                this.droneTelemetry_.longitude -= commandDto.Step;
                break;
            case Option.Right:
                this.droneTelemetry_.longitude += commandDto.Step;
                break;
            case Option.Ascend:
                this.droneTelemetry_.altitude = commandDto.Step;
                break;
            case Option.Descend:
                this.droneTelemetry_.altitude = commandDto.Step;
                break;
            case Option.Accelerate:
                this.droneTelemetry_.speed = commandDto.Step;
                break;
            case Option.Decelerate:
                this.droneTelemetry_.speed = commandDto.Step;
                break;
        }

        this.droneTelemetry_.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await Task.CompletedTask;

        return this.droneTelemetry_;
    }

    public async Task<DroneTelemetryDto> GetDroneTelemetryAsync(int DroneTelemetryId)
    {
        await Task.CompletedTask;
        return this.droneTelemetry_;
    }

    public async Task DeleteDroneTelemetryAsync(int DroneTelemetryId)
    {
        await Task.CompletedTask;
    }
}

public class VideoStreamer
{
    public async Task StreamVideoAsync(string folderPath, Action<string> sendChunk)
    {
        string[] jpegFiles = Directory.GetFiles(folderPath, "*.jpg");
        jpegFiles = jpegFiles.OrderBy(f => int.Parse(Path.GetFileNameWithoutExtension(f).Replace("frame_", ""))).ToArray();

        // Calculate the time interval between frames (20 images per second)
        double frameInterval = 1000 / 20; // Milliseconds

        // Iterate through JPEG files and send them as binary data
        while (true)
        {
            foreach (string jpegFile in jpegFiles)
            {

                byte[] imageData = File.ReadAllBytes(jpegFile);
                string base64String = Convert.ToBase64String(imageData);

                sendChunk(base64String);
                await Task.Delay(TimeSpan.FromMilliseconds(frameInterval));
            }
        }
    }
}
