using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public sealed class InvalidAccessTokenException : RequestException
{
    public InvalidAccessTokenException() : base(
        "Invalid access token!",
        ErrorType.InvalidToken,
        HttpStatusCode.BadRequest)
    {
    }
}