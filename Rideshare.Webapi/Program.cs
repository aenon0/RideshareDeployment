using Microsoft.OpenApi.Models;
using Rideshare.Application;
using Rideshare.Infrastructure;
using Rideshare.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using Rideshare.Application.Contracts.Infrastructure;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using Rideshare.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on port 8080
builder.WebHost.UseUrls("http://*:8080");
// BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rideshare API", Version = "v1" });

    // Define the OAuth2.0 scheme that's being used for the API
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            Array.Empty<string>()
        }
    });
});

// Dependency Injection configurations
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPersistenceAsync(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RiderPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("rider");
    });
});

var app = builder.Build();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rideshare API V1");
    c.RoutePrefix = "swagger"; // This will set the swagger UI route to 'http://localhost:8080/swagger'
    c.DocExpansion(DocExpansion.None);
});

// Middleware for redirecting HTTP to HTTPS could be added here if needed for production environments.

app.UseAuthentication();
app.UseAuthorization();

// CORS policy
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
});

// Map controllers
app.MapControllers();

app.Run();
