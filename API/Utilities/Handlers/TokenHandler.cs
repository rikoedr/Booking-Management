using API.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.Handlers;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(IEnumerable<Claim> claims)
    {
        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtService:SecretKey"]));
        SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: _configuration["JwtService:Issuer"],
                audience: _configuration["JwtService:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials);

        string? encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return encodedToken;
    }
}
