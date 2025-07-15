namespace Hub.Monetrik.Api.Responses.Despesas
{
    public class CadastrarDespesaResponse
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }        
        public int NumeroParcela { get; set; }
        public decimal ValorDaParcela { get; set; }
        public decimal TotalDeParcelas { get; set; }
        public decimal ValorTotal { get; set; }
        public string DataPagamento { get; set; }        
        public string Situacao { get; set; }
    }
}