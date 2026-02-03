using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using ProSphere.Extensions;

namespace ProSphere
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddMediatorServices();

            builder.Services.AddRepostoryAndUnitOfWork();
            
            builder.Services.AddFluentValidation();
            
            builder.Services.AddDbContextAndIdentity(builder.Configuration);
            
            builder.Services.AddJWT(builder.Configuration);
            
            builder.Services.AddEmailService(builder.Configuration);
            
            builder.Services.AddFileService(builder.Configuration);
            
            builder.Services.AddHangFire(builder.Configuration);
            
            builder.Services.AddHealthCheck(builder.Configuration);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
