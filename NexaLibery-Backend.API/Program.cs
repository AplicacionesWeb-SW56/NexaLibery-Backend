using NexaLibery_Backend.API.MultimediaContent.Application.Internal.CommandServices;
using NexaLibery_Backend.API.MultimediaContent.Application.Internal.QueryServices;
using NexaLibery_Backend.API.MultimediaContent.Domain.Repositories;
using NexaLibery_Backend.API.MultimediaContent.Domain.Services;
using NexaLibery_Backend.API.MultimediaContent.Interfaces.Persistence.EFC.Repositories;
using NexaLibery_Backend.API.Shared.Domain.Repositories;
using NexaLibery_Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using NexaLibery_Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using NexaLibery_Backend.API.Shared.Interfaces.ASP.Configuration;
using NexaLibery_Backend.API.IAM.Application.Internal.CommandServices;
using NexaLibery_Backend.API.IAM.Application.Internal.OutboundServices;
using NexaLibery_Backend.API.IAM.Application.Internal.QueryServices;
using NexaLibery_Backend.API.IAM.Domain.Repositories;
using NexaLibery_Backend.API.IAM.Domain.Services;
using NexaLibery_Backend.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using NexaLibery_Backend.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using NexaLibery_Backend.API.IAM.Infrastructure.Tokens.JWT.Services;
using NexaLibery_Backend.API.IAM.Interfaces.ACL;
using NexaLibery_Backend.API.IAM.Interfaces.ACL.Services;
using NexaLibery_Backend.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using NexaLibery_Backend.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using NexaLibery_Backend.API.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if(connectionString == null) throw new Exception("No connection string found");
        options
            .UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    else if (builder.Environment.IsProduction())
    {
        var connectionString = builder.Configuration.GetConnectionString("ProductionConnection");
        if(connectionString == null) throw new Exception("No connection string found");
        connectionString = FormatConnectionStringFromEnv.FromEnvToConnectionString(connectionString);
        options
            .UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
    }
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "NexaLibery-Backend.API",
                Version = "v1",
                Description = "NexaLibery Services API",
                TermsOfService = new Uri("https://acme-learning.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "NexaLibery",
                    Email = "contact@NexaLibery.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });

    });

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Publishing Bounded Context Injection Configuration
builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
builder.Services.AddScoped<ILibraryCommandService, LibraryCommandService>();
builder.Services.AddScoped<ILibraryQueryService, LibraryQueryService>();

builder.Services.AddScoped<IMultimediaRepository, MultimediaRepository>();
builder.Services.AddScoped<IMultimediaCommandService, MultimediaCommandService>();
builder.Services.AddScoped<IMultimediaQueryService, MultimediaQueryService>();

builder.Services.AddScoped<IPodcastRepository, PodcastRepository>();
builder.Services.AddScoped<IPodcastCommandService, PodcastCommandService>();
builder.Services.AddScoped<IPodcastQueryService, PodcastQueryService>();

// IAM Bounded Context Injection Configuration

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

// Add Authorization Middleware to Pipeline
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
