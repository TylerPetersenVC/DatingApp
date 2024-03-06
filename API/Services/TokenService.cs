using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); // Sets credentials using built in SigningCredentials Class which takes key generated from built in SymmetricSecurityKey Class

        var tokenDescriptor = new SecurityTokenDescriptor // Sets the descriptor object for the token using build in SecurityTokenDescriptor Class
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler(); // Creates a new object from the built in JwtSecurityTokenHandler Class

        var token = tokenHandler.CreateToken(tokenDescriptor); // Creates token using tokenHandler method => CreateToken which takes in tokenDescriptor

        return tokenHandler.WriteToken(token); // Returns a JWT in Compact Serialization Format
    }
}
