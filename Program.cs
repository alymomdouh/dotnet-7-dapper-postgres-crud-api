
using dotnet_7_dapper_postgres_crud_api.DbContext;
using dotnet_7_dapper_postgres_crud_api.Helpers;
using dotnet_7_dapper_postgres_crud_api.IRepositories;
using dotnet_7_dapper_postgres_crud_api.IServices;
using dotnet_7_dapper_postgres_crud_api.Repositories;
using dotnet_7_dapper_postgres_crud_api.Services;
using System.Text.Json.Serialization;

namespace dotnet_7_dapper_postgres_crud_api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // add services to DI container
            {
                var services = builder.Services;
                var env = builder.Environment;
                services.AddCors();
                services.AddControllers().AddJsonOptions(x =>
                {
                    // serialize enums as strings in api responses (e.g. Role)
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                    // ignore omitted parameters on models to enable optional params (e.g. User update)
                    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                // configure strongly typed settings object
                services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));

                // configure DI for application services
                services.AddSingleton<DataContext>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();
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

            // ensure database and tables exist
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                await context.Init();
            }

            // configure HTTP request pipeline
            {
                // global cors policy
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                // global error handler
                app.UseMiddleware<ErrorHandlerMiddleware>(); 
            }

            app.UseHttpsRedirection();

            app.UseAuthorization(); 

            app.MapControllers();

            app.Run();
        }
    }
}