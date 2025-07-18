using System.ComponentModel.DataAnnotations.Schema;
using Hub.Monetrik.Domain.Models.Entities.Parcelas;

namespace Hub.Monetrik.Domain.Models.Entities.Despesas
{
    [Table("despesas")]
    public class Despesa
    {
        public Despesa()
        {
            Parcelas = [];
        }

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

        [Column("total_parcelas")]
        public int TotalParcelas { get; set; }

        [Column("valor_total", TypeName = "decimal(18,2)")]
        public decimal ValorTotal { get; set; }

        [Column("data_vencimento")]
        public string DataInicioPagamento { get; set; }

        [Column("data_registro")]
        public DateTime DataRegistro { get; set; } = DateTime.Now;

        // Relacionamento com parcelas
        public virtual ICollection<Parcela> Parcelas { get; set; }
    }
}