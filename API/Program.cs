using System.Text;
using API;
using API.Data;
using API.Extensions;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        var app = builder.Build(); // Builds the WebApplication and assigns it to 'app' variable

        // Configure the HTTP request pipeline.
        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200")); // Adds a CORS middleware to your web application pipeline to allow cross domain requests.

        app.UseAuthentication(); // checks if token is valid
        app.UseAuthorization(); // if token is valid, what are you allowed to do?

        app.MapControllers(); // Adds endpoints for controller actions to the IEndpointRouteBuilder without specifying any routes.

        app.Run(); // Runs an application and block the calling thread until host shutdown.
    }
}