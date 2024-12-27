using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;
using System.Net;

namespace AeroLinker.Shared.Exceptions;

public sealed class InvalidPermissionsException : RequestException
{
    public InvalidPermissionsException() : base(
        "Don't have permission to perform this action",
        ErrorType.InvalidPermission,
        HttpStatusCode.Forbidden)
    {
    }
}