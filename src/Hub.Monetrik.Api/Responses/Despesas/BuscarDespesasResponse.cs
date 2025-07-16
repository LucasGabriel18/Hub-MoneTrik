using Hub.Monetrik.Domain.Models.Entities.Despesa;

namespace Hub.Monetrik.Api.Responses.Despesas
{
    public class BuscarDespesasResponse
    {
        public List<IGrouping<string, Despesa>> Despesas { get; set; }
    }
}