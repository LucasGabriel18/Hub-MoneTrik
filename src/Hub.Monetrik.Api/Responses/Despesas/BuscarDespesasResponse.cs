using Hub.Monetrik.Domain.Models.Entities.Despesas;

namespace Hub.Monetrik.Api.Responses.Despesas
{
    public class BuscarDespesasResponse
    {
        public List<DespesaDTO> Despesas { get; set; }
    }
    public class DespesaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
        public int TotalParcelas { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataRegistro { get; set; }
        public List<ParcelaDTO> Parcelas { get; set; }
    }

    public class ParcelaDTO
    {
        public int Id { get; set; }
        public int DespesaId { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public string DataVencimento { get; set; }
        public string Situacao { get; set; }
    }
}