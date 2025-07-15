using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Microsoft.EntityFrameworkCore;

namespace Hub.MoneTrik.Infrastructure.Context
{
    public class HubMonetrikContext : DbContext
    {
        public DbSet<Despesa> Despesas { get; set; }
        public HubMonetrikContext()
        {
        }

        public HubMonetrikContext(DbContextOptions<HubMonetrikContext> options) : base(options)
        {
        }
    }
}