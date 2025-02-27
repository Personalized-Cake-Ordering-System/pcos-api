using CusCake.Application.ViewModels.AuthModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CusCake.Application.Services;

public interface IJWTService
{
    string GenerateAccessToken(Guid id, string role);
    string GenerateRefreshToken(Guid id, string role);
    string RevokeToken(RevokeModel model);
}

public class JWTService : IJWTService
{
    private readonly AppSettings _settings;

    public JWTService(AppSettings settings)
    {
        _settings = settings;
    }

    public string GenerateAccessToken(Guid id, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWTOptions.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("id", id.ToString()),
            new Claim(ClaimTypes.Role, role)
        };
        var token = new JwtSecurityToken(
            issuer: _settings.JWTOptions.Issuer,
            audience: _settings.JWTOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddDays(10),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(Guid id, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWTOptions.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("id", id.ToString()),
            new Claim(ClaimTypes.Role, role)
        };
        var token = new JwtSecurityToken(
            issuer: _settings.JWTOptions.Issuer,
            audience: _settings.JWTOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddDays(1000),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string RevokeToken(RevokeModel model)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWTOptions.SecretKey));

            tokenHandler.ValidateToken(model.OldToken, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.JWTOptions.Issuer,
                ValidAudience = _settings.JWTOptions.Audience,
                IssuerSigningKey = securityKey
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var claims = jwtToken.Claims;

            var newToken = new JwtSecurityToken(
                issuer: _settings.JWTOptions.Issuer,
                audience: _settings.JWTOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return tokenHandler.WriteToken(newToken);
        }
        catch (SecurityTokenException ex)
        {
            throw new SecurityTokenException("Invalid or tampered token.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to refresh token.", ex);
        }
    }
}

