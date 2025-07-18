namespace Hub.Monetrik.Api.Responses.Parcelas
{
    public class ParcelaResponse
    {
        public int Id { get; set; }
        public int DespesaId { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public string DataVencimento { get; set; }
        public string Situacao { get; set; }       
    }
}