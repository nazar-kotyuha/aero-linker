using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public class QueryAlreadyExistException : RequestException
{
    public QueryAlreadyExistException(Guid queryId) : base(
        $"Query with ID: {queryId} was not registered or has expired",
        ErrorType.QueryExpired,
        HttpStatusCode.BadRequest)
    {
    }
}