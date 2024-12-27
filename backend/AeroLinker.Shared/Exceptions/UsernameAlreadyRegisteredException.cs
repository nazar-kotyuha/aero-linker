using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public sealed class UsernameAlreadyRegisteredException : RequestException
{
    public UsernameAlreadyRegisteredException() : base(
        "Username is already registered. Try another one",
        ErrorType.InvalidUsername,
        HttpStatusCode.BadRequest)
    {
    }
}