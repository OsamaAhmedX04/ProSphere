using Hangfire;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ProSphere.Extensions;
using ProSphere.Hubs.Chat;
using ProSphere.Hubs.Notification;

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

            builder.Services.AddBackgroundJobs();

            builder.Services.AddDbContextAndIdentity(builder.Configuration);

            builder.Services.AddJWT(builder.Configuration);

            builder.Services.AddEmailService(builder.Configuration);

            builder.Services.AddFileService(builder.Configuration);

            builder.Services.AddHangFire(builder.Configuration);

            builder.Services.AddHealthCheck(builder.Configuration);

            builder.Services.AddRateLimiting();

            builder.Services.AddCaching();

            builder.Services.AddSignalRWebSocket();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHangfireDashboard("/hangfire");
            }

            app.UseHttpsRedirection();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.MapHub<ChatHub>("/ChatHub");
            app.MapHub<NotificationHub>("/NotificationHub");

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
