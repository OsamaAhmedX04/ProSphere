using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProSphere.Data.Context;
using ProSphere.Domain.Entities;
using ProSphere.ExternalServices.Implementaions.Email;
using ProSphere.ExternalServices.Implementaions.JWT;
using ProSphere.ExternalServices.Interfaces.Email;
using ProSphere.ExternalServices.Interfaces.JWT;
using ProSphere.Features.Authentication.Commands.Register;
using ProSphere.Options;
using ProSphere.RepositoryManager.Implementations;
using ProSphere.RepositoryManager.Interfaces;
using Serilog;
using Supabase;
using System.Text;
using System.Threading.RateLimiting;

namespace ProSphere.Extensions
{
    public static class Depenedecies
    {
        public static IServiceCollection AddMediatorServices(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblyContaining<RegisterCommand>());

            return services;
        }

        public static IServiceCollection AddDbContextAndIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
            ));

            // Register Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders(); // For email confirm, reset password

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });

            return services;
        }

        public static IServiceCollection AddRepostoryAndUnitOfWork(this IServiceCollection services)
        {
            // Register UOW
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            // Validators Service
            services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

            return services;
        }

        public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration configuration)
        {
            // Register JWT
            services.AddScoped<IJWTService,JWTService>();

            var jwtSettings = configuration.GetSection("JWT");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                };
            });


            services.AddOptions<JWTOptions>()
                .Bind(jwtSettings)
                .ValidateOnStart();

            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            // Register SendGrid
            services.AddOptions<SendGridOptions>()
                .Bind(configuration.GetSection("SendGridSettings"))
                .ValidateOnStart();

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        public static IServiceCollection AddFileService(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind Supabase settings from appsettings.json
            services.AddOptions<SupaOptions>()
                .Bind(configuration.GetSection("Supabase"))
                .ValidateOnStart();

            // Register Supabase.Client as singleton
            services.AddSingleton<Client>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<SupaOptions>>().Value;
                return new Client(settings.Url, settings.ServiceKey, new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = true
                });
            });

            return services;
        }

        public static IServiceCollection AddHangFire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(option =>
            {
                option
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                //.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
                .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddHangfireServer();

            return services;
        }

        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(
                    configuration.GetConnectionString("DefaultConnection")!,
                    name: "Development-Database")
                .AddSendGrid(configuration["SendGridSettings:ApiKey"]!);

            return services;
        }

        public static IServiceCollection AddRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // Custom rejection response
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.HttpContext.Response.WriteAsJsonAsync(new
                    {
                        error = "Too many requests. Please try again later."
                    }, token);
                };

                // Global IP-based limiter
                options.GlobalLimiter =
                    PartitionedRateLimiter.Create<HttpContext, string>(context =>
                    {
                        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                        return RateLimitPartition.GetFixedWindowLimiter(
                            ip,
                            _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = 25,
                                Window = TimeSpan.FromMinutes(1),
                                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                                QueueLimit = 0
                            });
                    });

                // Named policies
                options.AddFixedWindowLimiter("strict", opt =>
                {
                    opt.PermitLimit = 3;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueLimit = 0;
                });
            });

            return services;
        }

        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            });

            return hostBuilder;
        }
    }
}
