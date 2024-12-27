namespace AeroLinker.DroneHub.BLL.Exceptions;

public class ConnectionFileNotFound : Exception
{
    public ConnectionFileNotFound(string path)
        : base($"Connection File with path: {path} was not found")
    {
    }
}