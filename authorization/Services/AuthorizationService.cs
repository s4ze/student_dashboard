using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using authorization.Data;
using Microsoft.IdentityModel.Tokens;

namespace authorization.Services;

public class AuthorizationService(AuthenticationService authenticationService, DataContext context)
{
    private readonly AuthenticationService _authenticationService = authenticationService;
    private readonly DataContext _context = context;
    public bool ValidateToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            handler.ValidateToken(token, JwtOptions.TokenValidationParameters, out SecurityToken validatedToken);
            if (validatedToken is not JwtSecurityToken jwtSecurityToken)
                return false;
            var issuedAt = jwtSecurityToken.Payload.IssuedAt;
            if (issuedAt.CompareTo(DateTime.UtcNow) > 0)
                return false;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
    public string GenerateAccessToken(Guid userId, bool admin = false)
    {
        List<Claim> claims = [new(JwtClaims.UserIdClaimName, userId.ToString())];
        if (admin == true)
            claims.Add(new(JwtClaims.AdminClaimName, "true"));

        JwtSecurityToken accessTokenValues = new(
                claims: claims,
                issuer: JwtOptions.ISSUER,
                audience: JwtOptions.AUDIENCE,
                signingCredentials: new SigningCredentials(
                    JwtOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256
                ),
                expires: DateTime.UtcNow.AddMinutes(30)
            );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(accessTokenValues);

        return accessToken;
    }
    public string GenerateRefreshToken(Guid userId)
    {
        var user = _authenticationService.GetUserById(userId);
        List<Claim> claims = [new(JwtClaims.UserIdClaimName, userId.ToString())];
        if (user.Role == "Admin")
            claims.Add(new(JwtClaims.AdminClaimName, "true"));

        JwtSecurityToken refreshTokenValues = new(
                claims: claims,
                issuer: JwtOptions.ISSUER,
                audience: JwtOptions.AUDIENCE,
                signingCredentials: new SigningCredentials(
                    JwtOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256
                ),
                expires: DateTime.UtcNow.AddDays(15)
            );

        var refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshTokenValues);
        user.RefreshToken = refreshToken;
        _context.SaveChanges();
        // if user is null - unexpected result in system
        return refreshToken;
    }
    public bool SetRefreshToken(Guid guid, string refreshToken)
    {
        try
        {
            var user = _authenticationService.GetUserById(guid);
            user.RefreshToken = refreshToken;
            _context.SaveChanges();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}
