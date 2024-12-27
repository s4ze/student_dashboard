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
    public string GenerateToken(Guid userId, string role = "student", bool isRefreshToken = false)
    {
        List<Claim> claims = [
            new(JwtClaims.UserIdClaimName, userId.ToString()),
            new(JwtClaims.RoleClaimName, role)
        ];

        JwtSecurityToken tokenValues = new(
                claims: claims,
                issuer: JwtOptions.ISSUER,
                audience: JwtOptions.AUDIENCE,
                signingCredentials: new SigningCredentials(
                    JwtOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256
                ),
                expires: isRefreshToken ? DateTime.UtcNow.AddDays(15) : DateTime.UtcNow.AddMinutes(30)
            );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenValues);
        if (isRefreshToken) SetRefreshToken(userId, token);

        return token;
    }
    public bool SetRefreshToken(Guid userId, string refreshToken)
    {
        try
        {
            var user = _authenticationService.GetUserById(userId);
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
