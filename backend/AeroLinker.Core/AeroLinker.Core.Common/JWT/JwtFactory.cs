﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AeroLinker.Core.Common.Interfaces;
using AeroLinker.Core.Common.Security;
using AeroLinker.Shared.Exceptions;

namespace AeroLinker.Core.Common.JWT;

public sealed class JwtFactory : IJwtFactory
{
    public const string IdClaimName = "id";
    private const string UserNameClaimName = "username";
    private readonly JwtIssuerOptions _jwtOptions;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
    {
        ThrowIfInvalidOptions(jwtOptions.Value!);

        _jwtOptions = jwtOptions.Value!;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<string> GenerateAccessTokenAsync(int id, string userName, string email)
    {
        var identity = GenerateClaimsIdentity(id, userName);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
            new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
            identity.FindFirst(IdClaimName)
        };

        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            _jwtOptions.NotBefore,
            _jwtOptions.Expiration,
            _jwtOptions.SigningCredentials);

        return _jwtSecurityTokenHandler.WriteToken(jwt)!;
    }

    public string GenerateRefreshToken() => SecurityUtils.GenerateRandomSalt();
    
    public int GetUserIdFromToken(string accessToken)
    {
        var signingKey = _jwtOptions.SecretJwtKey;
        var claimsPrincipal = GetPrincipalFromToken(accessToken, signingKey);

        if (claimsPrincipal is null)
        {
            throw new InvalidAccessTokenException();
        }

        return int.Parse(claimsPrincipal.Claims.First(c => c.Type == IdClaimName).Value);
    }

    private static ClaimsIdentity GenerateClaimsIdentity(int id, string userName)
    {
        return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
        {
            new Claim(IdClaimName, id.ToString()),
            new Claim(UserNameClaimName, userName)
        });
    }

    private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(options.SigningCredentials);
        ArgumentNullException.ThrowIfNull(options.JtiGenerator);

        if (options.Lifetime <= TimeSpan.Zero)
        {
            throw new ArgumentException("Lifetime must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.Lifetime));
        }
    }

    private ClaimsPrincipal? GetPrincipalFromToken(string token, string signingKey)
        => _jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
            ValidateLifetime = false
        }, out _);


    private long ToUnixEpochDate(DateTime date)
    {
        var unixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        return (long)Math.Round((date.ToUniversalTime() - unixEpoch).TotalSeconds);
    }
}