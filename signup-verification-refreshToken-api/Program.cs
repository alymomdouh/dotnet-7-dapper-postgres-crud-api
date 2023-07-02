
using Microsoft.EntityFrameworkCore;
using signup_verification_refreshToken_api.Authorization;
using signup_verification_refreshToken_api.DbContext;
using signup_verification_refreshToken_api.Helpers;
using signup_verification_refreshToken_api.IServices;
using signup_verification_refreshToken_api.Services;
using System.Text.Json.Serialization;

namespace signup_verification_refreshToken_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // add services to DI container
            {
                var services = builder.Services;
                var env = builder.Environment;

                services.AddDbContext<DataContext>();
                services.AddCors();
                services.AddControllers().AddJsonOptions(x =>
                {
                    // serialize enums as strings in api responses (e.g. Role)
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                services.AddSwaggerGen();

                // configure strongly typed settings object
                services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

                // configure DI for application services
                services.AddScoped<IJwtUtils, JwtUtils>();
                services.AddScoped<IAccountService, AccountService>();
                services.AddScoped<IEmailService, EmailService>();
            }

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // migrate any database changes on startup (includes initial db creation)
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dataContext.Database.Migrate();
            }

            // configure HTTP request pipeline
            {
                // generated swagger json and swagger ui middleware
                app.UseSwagger();
                app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Sign-up and Verification API"));

                // global cors policy
                app.UseCors(x => x
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

                // global error handler
                app.UseMiddleware<ErrorHandlerMiddleware>();

                // custom jwt auth middleware
                app.UseMiddleware<JwtMiddleware>();

                 
            } 

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}