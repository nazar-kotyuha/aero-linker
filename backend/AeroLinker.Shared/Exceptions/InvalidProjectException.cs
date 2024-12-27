using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public sealed class InvalidProjectException : RequestException
{
    public InvalidProjectException() : base(
        "Such project does not exist!",
        ErrorType.InvalidProject,
        HttpStatusCode.BadRequest)
    {
    }
}