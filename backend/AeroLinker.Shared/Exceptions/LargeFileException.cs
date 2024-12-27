using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public class LargeFileException : RequestException
{
    public LargeFileException(string maxSize) : base(
        $"The file size should not exceed {maxSize}",
        ErrorType.LargeFile,
        HttpStatusCode.RequestEntityTooLarge)
    {
    }
}