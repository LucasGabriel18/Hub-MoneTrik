using Hub.MoneTrik.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Hub.MoneTrik.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HubMonetrikContext>
    {
        public HubMonetrikContext CreateDbContext(string[] args)
        {
            // Configuração do builder
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);
            
            // Só adiciona UserSecrets se estiver em desenvolvimento
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                configBuilder.AddUserSecrets<DesignTimeDbContextFactory>();
            }

            IConfigurationRoot configuration = configBuilder.Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=localhost;Database=hubmonetrik;User=dev;Password=dev123;";

            var optionsBuilder = new DbContextOptionsBuilder<HubMonetrikContext>();
            
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                mysqlOptions => 
                {
                    mysqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });

            return new HubMonetrikContext(optionsBuilder.Options);
        }
    }
}