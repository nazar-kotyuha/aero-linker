using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public sealed class EmailAlreadyRegisteredException : RequestException
{
    public EmailAlreadyRegisteredException() : base(
        "Email is already registered. Try another one",
        ErrorType.InvalidEmail,
        HttpStatusCode.BadRequest)
    {
    }
}