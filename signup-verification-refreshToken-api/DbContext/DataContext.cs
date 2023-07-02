using Microsoft.EntityFrameworkCore;
using signup_verification_refreshToken_api.Entities;

namespace signup_verification_refreshToken_api.DbContext
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }
        // migration commands 
        //dotnet ef migrations add InitialCreate
        //dotnet ef migrations remove
        //dotnet ef database update
    }
}
