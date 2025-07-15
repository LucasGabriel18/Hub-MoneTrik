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
            var optionsBuilder = new DbContextOptionsBuilder<HubMonetrikContext>();
            var connectionString = "Server=localhost;Database=hubmonetrik;User=dev;Password=dev123;";

            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                options => options.EnableRetryOnFailure());

            return new HubMonetrikContext(optionsBuilder.Options);
        }
    }
}