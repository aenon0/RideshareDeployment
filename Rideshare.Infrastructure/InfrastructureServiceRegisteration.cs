using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rideshare.Application.Contracts.Infrastructure;
using Rideshare.Infrastructure.JwtService;

namespace Rideshare.Infrastructure;

public static class InfrastrucureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ISmsService, SmsService>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options =>
          {
              var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;

              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = jwtSettings.Issuer,
                  ValidAudience = jwtSettings.Audience,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
              };
          });





        return services;
    }

    // public static IServiceCollection RegisterAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
    //     services.AddScoped<IJwtGenerator, JwtGenerator>();

    //     services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    //       .AddJwtBearer(options =>
    //       {
    //           var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;

    //           options.TokenValidationParameters = new TokenValidationParameters
    //           {
    //               ValidateIssuer = true,
    //               ValidateAudience = true,
    //               ValidateLifetime = true,
    //               ValidateIssuerSigningKey = true,
    //               ValidIssuer = jwtSettings.Issuer,
    //               ValidAudience = jwtSettings.Audience,
    //               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    //           };
    //       });

    //     return services;
    // }

}