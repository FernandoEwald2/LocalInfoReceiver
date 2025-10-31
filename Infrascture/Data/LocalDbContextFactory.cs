using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Data
{
    public class LocalDbContextFactory : IDesignTimeDbContextFactory<LocalDbContext>
    {
        public LocalDbContext CreateDbContext(string[] args)
        {
            // Lê as variáveis de ambiente diretamente
            var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Ew@ldP@$$123";
            var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "localzap";
            var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "db";

            var connectionString = $"Host={host};Port=5432;Database={dbName};Username={dbUser};Password={dbPassword}";

            var optionsBuilder = new DbContextOptionsBuilder<LocalDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new LocalDbContext(optionsBuilder.Options);
        }
    }
}
