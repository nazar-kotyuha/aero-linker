﻿namespace AeroLinker.Core.Common.Interfaces;

public interface IJwtFactory
{
    Task<string> GenerateAccessTokenAsync(int id, string userName, string email);
    string GenerateRefreshToken();
    int GetUserIdFromToken(string accessToken);
}