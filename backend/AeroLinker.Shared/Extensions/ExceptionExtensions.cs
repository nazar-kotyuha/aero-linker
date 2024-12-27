using System.Net;
using AeroLinker.Shared.DTO.Error;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Extensions;

public static class ExceptionExtensions
{
    public static (ErrorDetailsDto, HttpStatusCode) GetErrorDetailsAndStatusCode(this Exception exception)
    {
        return exception switch
        {
            RequestException e => (new ErrorDetailsDto(e.Message, e.ErrorType), e.StatusCode),
            _ => (new ErrorDetailsDto(exception.Message, ErrorType.Internal), HttpStatusCode.InternalServerError)
        };
    }
}