using Hub.Monetrik.Api.Responses.Parcelas;

namespace Hub.Monetrik.Api.Responses.Despesas
{
    public class BuscarDespesaPorIdResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
        public decimal ValorTotal { get; set; }
        public int TotalParcelas { get; set; }
        public string DataRegistro { get; set; }
        public List<ParcelaResponse> Parcelas { get; set; }
    }
}