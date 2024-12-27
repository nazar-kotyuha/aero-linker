using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public sealed class InvalidRefreshTokenException : RequestException
{
    public InvalidRefreshTokenException() : base(
        "Invalid refresh token!",
        ErrorType.InvalidToken,
        HttpStatusCode.BadRequest)
    {
    }
}