namespace AeroLinker.Shared.Enums;

public enum ErrorType
{
    InvalidEmail = 1,
    InvalidUsername,
    InvalidPassword,
    InvalidEmailOrPassword,
    NotFound,
    Internal,
    InvalidToken,
    InvalidProject,
    InvalidDroneConnection,
    LargeFile,
    InvalidFileFormat,
    RefreshTokenExpired,
    SqlSyntax,
    InvalidQuery,
    QueryExpired,
    HttpRequest,
    InvalidPermission
}