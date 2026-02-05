using Hangfire;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ProSphere.Extensions;

namespace ProSphere
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.AddSerilog();

            builder.Services.AddControllers();

            builder.Services.AddBusinessServices();

            builder.Services.AddMediatorServices();

            builder.Services.AddRepostoryAndUnitOfWork();

            builder.Services.AddFluentValidation();

            builder.Services.AddDbContextAndIdentity(builder.Configuration);

            builder.Services.AddJWT(builder.Configuration);

            builder.Services.AddEmailService(builder.Configuration);

            builder.Services.AddFileService(builder.Configuration);

            builder.Services.AddHangFire(builder.Configuration);

            builder.Services.AddHealthCheck(builder.Configuration);

            builder.Services.AddRateLimiting();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHangfireDashboard("/hangfire");

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
