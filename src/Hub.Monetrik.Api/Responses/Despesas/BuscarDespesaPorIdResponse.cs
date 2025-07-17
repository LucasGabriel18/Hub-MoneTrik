namespace Hub.Monetrik.Api.Responses.Despesas
{
    public class BuscarDespesaPorIdResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
        public decimal ValorParcela { get; set; }
        public int NumeroParcela { get; set; }
        public int TotalParcelas { get; set; }        
        public decimal ValorTotal { get; set; }        
        public string DataPagamento { get; set; }
        public string DataRegistro { get; set; }       
        public string Situacao { get; set; }
    }
}