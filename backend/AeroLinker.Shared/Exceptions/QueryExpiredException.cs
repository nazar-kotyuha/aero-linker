using System.Net;
using AeroLinker.Shared.Enums;
using AeroLinker.Shared.Exceptions.Abstract;

namespace AeroLinker.Shared.Exceptions;

public class QueryExpiredException : RequestException
{
    public QueryExpiredException(Guid queryId) : base(
        $"Query with ID: {queryId} is already exist in project",
        ErrorType.InvalidQuery,
        HttpStatusCode.BadRequest)
    {
    }
}