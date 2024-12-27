using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public sealed class ExpiredRefreshTokenException : RequestException
{
    public ExpiredRefreshTokenException() : base(
        "Refresh token expired.",
        ErrorType.RefreshTokenExpired,
        HttpStatusCode.Forbidden)
    {
    }
}