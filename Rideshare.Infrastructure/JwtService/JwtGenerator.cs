using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rideshare.Application.Contracts.Infrastructure;
using Rideshare.Domain.Entities;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using System.Collections.Generic;


namespace Rideshare.Infrastructure.JwtService;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtSettings _jwtSettings;
    public JwtGenerator(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
        Console.WriteLine(_jwtSettings);
    }
    public string Generate(Rider rider)
    {

        // Generate signing credentials
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256Signature
        );

        // Create claims
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, rider.Id.ToString()),
            new Claim(ClaimTypes.Role, "rider")
        };

        // Generate the token
        var expirationTime = DateTime.UtcNow.AddHours(6);
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: expirationTime, 
            signingCredentials: signingCredentials,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
