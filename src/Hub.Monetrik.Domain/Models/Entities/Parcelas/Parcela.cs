using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Hub.Monetrik.Domain.Models.Entities.Despesas;

namespace Hub.Monetrik.Domain.Models.Entities.Parcelas
{
    [Table("parcelas")]
    public class Parcela
    {
        [Column("id")]
        public int Id { get; private set; }

        [Column("despesa_id")]
        public int DespesaId { get; set; }

        [ForeignKey("DespesaId")]
        [JsonIgnore]
        public Despesa Despesa { get; set; }

        [Column("numero_parcela")]
        public int NumeroParcela { get; set; }

        [Column("valor_parcela", TypeName = "decimal(18,2)")]
        public decimal ValorParcela { get; set; }

        [Column("data_vencimento")]
        public string DataVencimento { get; set; }

        [Column("situacao")]
        public string Situacao { get; set; }
    }
}