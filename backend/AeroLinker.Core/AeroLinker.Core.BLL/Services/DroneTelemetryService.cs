/*using AeroLinker.DroneHub.BLL.Interfaces;
using AeroLinker.Shared.DTO.Drone;

namespace AeroLinker.Core.BLL.Services;
public sealed class DroneTelemetryService : IDroneTelemetryService
{
    private readonly DroneTelemetryDto droneTelemetry_ = new DroneTelemetryDto
    {
        Altitude = 0,
        BatteryLevel = 100,
        Latitude = 50.6198, // Initial latitude
        Longitude = 26.2514, // Initial longitude
        Speed = 0,
        Status = "on ground",
        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
    };

    public DroneTelemetryService()
    {
    }

    public async Task<DroneTelemetryDto> UpdateDroneTelemetryAsync(CommandDto commandDto)
    {
        switch (commandDto.Option)
        {
            case Option.Up:
                this.droneTelemetry_.Latitude += commandDto.Step;
                break;
            case Option.Down:
                this.droneTelemetry_.Latitude -= commandDto.Step;
                break;
            case Option.Left:
                this.droneTelemetry_.Longitude -= commandDto.Step;
                break;
            case Option.Right:
                this.droneTelemetry_.Longitude += commandDto.Step;
                break;
            case Option.Ascend:
                this.droneTelemetry_.Altitude = commandDto.Step;
                break;
            case Option.Descend:
                this.droneTelemetry_.Altitude = commandDto.Step;
                break;
            case Option.Accelerate:
                this.droneTelemetry_.Speed = commandDto.Step;
                break;
            case Option.Decelerate:
                this.droneTelemetry_.Speed = commandDto.Step;
                break;
        }

        this.droneTelemetry_.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        // Simulate async operation
        await Task.CompletedTask;

        return this.droneTelemetry_;
    }

    public async Task<DroneTelemetryDto> GetDroneTelemetryAsync(int DroneTelemetryId)
    {
        // Simulate async operation
        await Task.CompletedTask;
        return this.droneTelemetry_;
    }

    public async Task DeleteDroneTelemetryAsync(int DroneTelemetryId)
    {
        // Simulate async operation
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

                // Output the Base64 string
                //Console.WriteLine(base64String);
                sendChunk(base64String);
                //return;
                // Pause for the required time interval
                await Task.Delay(TimeSpan.FromMilliseconds(frameInterval));
            }
        }
    }
}
*/