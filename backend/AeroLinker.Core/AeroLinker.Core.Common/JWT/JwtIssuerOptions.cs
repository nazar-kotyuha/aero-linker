﻿using Microsoft.IdentityModel.Tokens;

namespace AeroLinker.Core.Common.JWT;

public sealed class JwtIssuerOptions
{
    /// <summary>
    /// 4.1.1.  "iss" (Issuer) Claim - The "iss" (issuer) claim identifies
    /// the principal that issued the JWT.
    /// </summary>
    public string Issuer { get; set; } = null!;

    /// <summary>
    /// 4.1.2.  "sub" (Subject) Claim - The "sub" (subject) claim identifies
    /// the principal that is the subject of the JWT.
    /// </summary>
    public string Subject { get; set; } = null!;

    /// <summary>
    /// 4.1.3.  "aud" (Audience) Claim - The "aud" (audience) claim identifies
    /// the recipients that the JWT is intended for.
    /// </summary>
    public string Audience { get; set; } = null!;

    /// <summary>
    /// 4.1.4.  "exp" (Expiration Time) Claim - The "exp" (expiration time) claim identifies
    /// the expiration time on or after which the JWT MUST NOT be accepted for processing.
    /// </summary>
    public DateTime Expiration => IssuedAt.Add(Lifetime);

    /// <summary>
    /// 4.1.5.  "nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time
    /// before which the JWT MUST NOT be accepted for processing.
    /// </summary>
    public DateTime NotBefore => DateTime.UtcNow;

    /// <summary>
    /// 4.1.6.  "iat" (Issued At) Claim - The "iat" (issued at) claim identifies the time
    /// at which the JWT was issued.
    /// </summary>
    public DateTime IssuedAt => DateTime.UtcNow;

    /// <summary>
    /// Set the timespan the token will be valid for (default is 1 hour)
    /// </summary>
    public TimeSpan Lifetime { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// "jti" (JWT ID) Claim (default ID is a GUID)
    /// </summary>
    public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());

    /// <summary>
    /// The signing key to use when generating tokens.
    /// </summary>
    public SigningCredentials SigningCredentials { get; set; } = null!;

    /// <summary>
    /// Storing secret key in appsettings for testing purposes only!
    /// </summary>
    public string SecretJwtKey { get; set; } = null!;
}