using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Gets or sets a boolean that controls if validation of the SecurityKey that signed the securityToken is called.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])), // Gets or sets the SecurityKey that is to be used for signature validation.
                    ValidateIssuer = false, // Gets or sets a boolean to control if the issuer will be validated during token validation.
                    ValidateAudience = false // Gets or sets a boolean to control if the audience will be validated during token validation.
                };
            });

        return services;
    }
}
