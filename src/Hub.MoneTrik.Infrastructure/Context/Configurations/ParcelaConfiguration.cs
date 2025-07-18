using Hub.Monetrik.Domain.Models.Entities.Parcelas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hub.MoneTrik.Infrastructure.Context.Configurations
{
    public class ParcelaConfiguration : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.ToTable("parcelas");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DespesaId)
                .HasColumnName("despesa_id")
                .IsRequired();

            builder.Property(x => x.NumeroParcela)
                .HasColumnName("numero_parcela")
                .IsRequired();

            builder.Property(x => x.ValorParcela)
                .HasColumnName("valor_parcela")
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.DataVencimento)
                .HasColumnName("data_vencimento")
                .IsRequired();

            builder.Property(x => x.Situacao)
                .HasColumnName("situacao")
                .IsRequired();

            // Relacionamento
            builder.HasOne(p => p.Despesa)
                .WithMany(d => d.Parcelas)
                .HasForeignKey(p => p.DespesaId);
        }
    }
}