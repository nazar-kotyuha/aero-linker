using System.Net;
using AeroLinker.Shared.Exceptions.Abstract;
using AeroLinker.Shared.Enums;

namespace AeroLinker.Shared.Exceptions;

public class InvalidEmailOrPasswordException : RequestException
{
    public InvalidEmailOrPasswordException() : base(
        "Invalid email or password.",
        ErrorType.InvalidEmailOrPassword,
        HttpStatusCode.BadRequest)
    {
    }
}