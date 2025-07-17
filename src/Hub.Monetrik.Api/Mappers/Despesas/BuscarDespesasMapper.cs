using Hub.Monetrik.Api.Responses.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Despesa;

namespace Hub.Monetrik.Api.Mappers.Despesas
{
    public class BuscarDespesasMapper
    {
        public static BuscarDespesasResponse Map(List<IGrouping<string, Despesa>> despesas)
        {
            return new BuscarDespesasResponse
            {
                Despesas = despesas
            };
        }
    }
}