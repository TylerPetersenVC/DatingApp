using API.Data;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors(); // Adds cross-origin resource sharing services to the specified IServiceCollection.
        services.AddScoped<ITokenService, TokenService>(); // Adds a scoped service of the type specified in ITokenService with an implementation type specified in TokenService to the specified IServiceCollection.

        return services;
    }
}
