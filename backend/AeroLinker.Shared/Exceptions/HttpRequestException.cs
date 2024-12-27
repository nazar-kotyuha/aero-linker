using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public sealed class HttpRequestException : RequestException
{
    public HttpRequestException(string message) : base(
        message,
        ErrorType.HttpRequest,
        HttpStatusCode.BadRequest)
    {
    }
}