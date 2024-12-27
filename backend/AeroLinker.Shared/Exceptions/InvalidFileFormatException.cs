using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public class InvalidFileFormatException : RequestException
{
    public InvalidFileFormatException(string types) : base(
        $"Invalid file type, need {types}",
        ErrorType.InvalidFileFormat,
        HttpStatusCode.UnsupportedMediaType)
    {
    }
}