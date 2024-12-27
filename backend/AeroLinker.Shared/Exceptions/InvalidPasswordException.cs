using System.Net;
using AeroLinker.Shared.Exceptions.Abstract;
using AeroLinker.Shared.Enums;

namespace AeroLinker.Shared.Exceptions;

public class InvalidPasswordException : RequestException
{
    public InvalidPasswordException() : base(
        "Invalid password.",
        ErrorType.InvalidPassword,
        HttpStatusCode.BadRequest)
    {
    }
}