using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hub.MoneTrik.Infrastructure.Context.Configurations
{
    public class DespesaConfiguration : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.ToTable("despesas");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Titulo)
                .HasColumnName("titulo")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao");

            builder.Property(x => x.Categoria)
                .HasColumnName("categoria")
                .IsRequired();

            builder.Property(x => x.Tipo)
                .HasColumnName("tipo")
                .IsRequired();

            builder.Property(x => x.ValorParcela)
                .HasColumnName("valor_parcela")
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.NumeroParcela)
                .HasColumnName("numero_parcela")
                .IsRequired();

            builder.Property(x => x.TotalParcelas)
                .HasColumnName("total_parcelas")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.ValorTotal)
                .HasColumnName("valor_total")
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.DataInicioPagamento)
                .HasColumnName("data_vencimento")
                .IsRequired();

            builder.Property(x => x.DataRegistro)
                .HasColumnName("data_registro")
                .IsRequired();

            builder.Property(x => x.Situacao)
                .HasColumnName("situacao")
                .IsRequired();
        }
    }
}