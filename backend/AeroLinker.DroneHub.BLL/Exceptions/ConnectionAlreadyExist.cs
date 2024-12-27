namespace AeroLinker.DroneHub.BLL.Exceptions;

public class ConnectionAlreadyExist : Exception
{
    public ConnectionAlreadyExist()
        : base("Console already has connection to db, try another one or delete current db")
    {
    }
}