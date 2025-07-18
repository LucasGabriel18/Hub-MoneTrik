using Hub.Monetrik.Domain.Models.Entities.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Parcelas;
using Microsoft.EntityFrameworkCore;

namespace Hub.MoneTrik.Infrastructure.Context
{
    public class HubMonetrikContext : DbContext
    {
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public HubMonetrikContext()
        {
        }

        public HubMonetrikContext(DbContextOptions<HubMonetrikContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do relacionamento entre Despesa e Parcelas
            modelBuilder.Entity<Despesa>()
                .HasMany(d => d.Parcelas)
                .WithOne(p => p.Despesa)
                .HasForeignKey(p => p.DespesaId);
        }
    }
}