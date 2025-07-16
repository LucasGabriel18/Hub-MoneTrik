using Hub.Monetrik.Api.Responses.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Despesa;

namespace Hub.Monetrik.Api.Mappers.Despesas
{
    public class BuscarDespesaPorIdMapper
    {
        public static BuscarDespesaPorIdResponse Map(Despesa despesa)
        {
            return new BuscarDespesaPorIdResponse
            {
                Id = despesa.Id,
                Titulo = despesa.Titulo,
                Descricao = despesa.Descricao,
                Categoria = despesa.Categoria,
                Tipo = despesa.Tipo,
                ValorParcela = despesa.ValorParcela,
                NumeroParcela = despesa.NumeroParcela,
                TotalParcelas = despesa.TotalParcelas,
                ValorTotal = despesa.ValorTotal,
                DataPagamento = despesa.DataInicioPagamento,
                DataRegistro = despesa.DataRegistro.ToString("dd/MM/yyyy"),
                Situacao = despesa.Situacao,
            };
        }
    }
}