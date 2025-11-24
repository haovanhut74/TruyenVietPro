using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TruyenHayPro.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Build path to the startup project's appsettings (adjust if your solution layout differs)
            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "TruyenHayPro.WebAPI"));

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath) // requires Microsoft.Extensions.Configuration.FileExtensions
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var conn = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(conn))
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found in WebAPI/appsettings.json");

            // Use the generic DbContextOptionsBuilder<AppDbContext> to avoid ambiguous UseSqlServer overloads
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(conn,
                sqlOptions =>   
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                });

            // AppDbContext constructor accepts tenantProvider and logger as optional (null is OK)
            return new AppDbContext(optionsBuilder.Options, tenantProvider: null, logger: null);
        }
    }
}