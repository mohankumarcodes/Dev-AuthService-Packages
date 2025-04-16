using Dev.AuthService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Text;



namespace Dev.AuthService.Extensions
{
    /// <summary>
    /// Provides extension method to register authentication service and JWT settings.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers DevXAuthService with JWT configuration and DI.
        /// </summary>
        public static IServiceCollection AddDevAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your service
            services.AddScoped<IAuthService, AuthServiceClass>();

            // JWT Setup
            var key = configuration["Jwt:Key"];
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

    }
}
