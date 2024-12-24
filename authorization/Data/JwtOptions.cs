using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace authorization.Data;
public static class JwtOptions
{
    public const string ISSUER = JwtData.Issuer; // издатель токена
    public const string AUDIENCE = JwtData.Audience; // потребитель токена
    public const string KEY = JwtData.Key;   // ключ для шифрования
    public static TokenValidationParameters TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = ISSUER,
        ValidateAudience = true,
        ValidAudience = AUDIENCE,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = GetSymmetricSecurityKey(),
    };
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}