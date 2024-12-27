using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;
using System.Net;

namespace AeroLinker.Shared.Exceptions;

public sealed class InvalidDroneConnectionException : RequestException
{
    public InvalidDroneConnectionException() : base(
        "Such drone connection does not exist!",
        ErrorType.InvalidDroneConnection,
        HttpStatusCode.BadRequest)
    {
    }
}