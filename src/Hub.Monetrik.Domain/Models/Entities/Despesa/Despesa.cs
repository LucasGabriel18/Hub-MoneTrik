using Hub.MoneTrik.Infrastructure.Enums.Despesas;

namespace Hub.Monetrik.Domain.Models.Entities.Despesa
{
    public class Despesa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public ECategoriaDespesas Categoria { get; set; }
        public ETipoDespesas Tipo { get; set; }
        public decimal ValorParcela { get; set; }
        public int NumeroParcela { get; set; }
        public decimal TotalParcelas { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataInicioPagamento { get; set; }        
        public DateTime DataRegistro { get; set; } = DateTime.Now;
        public ESituacaoDespesa Situacao { get; set; } = ESituacaoDespesa.Pendente;
    }
}