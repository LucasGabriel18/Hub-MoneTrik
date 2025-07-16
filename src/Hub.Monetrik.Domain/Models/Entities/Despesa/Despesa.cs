using System.ComponentModel.DataAnnotations.Schema;
using Hub.Monetrik.Domain.Enums.Despesas;

namespace Hub.Monetrik.Domain.Models.Entities.Despesa
{
    [Table("despesas")]
    public class Despesa
    {
        [Column("id")]
        public int Id { get; private set; }

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("categoria")]
        public string Categoria { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("valor_parcela", TypeName = "decimal(18,2)")]
        public decimal ValorParcela { get; set; }

        [Column("numero_parcela")]
        public int NumeroParcela { get; set; }

        [Column("total_parcelas")]
        public int TotalParcelas { get; set; }

        [Column("valor_total", TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        [Column("data_vencimento")]
        public string DataInicioPagamento { get; set; }

        [Column("data_registro")]
        public DateTime DataRegistro { get; set; } = DateTime.Now;

        [Column("situacao")]
        public string Situacao { get; set; }
    }
}