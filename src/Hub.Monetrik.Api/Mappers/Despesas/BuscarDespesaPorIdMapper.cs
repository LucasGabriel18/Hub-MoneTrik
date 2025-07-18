using Hub.Monetrik.Api.Mappers.Parcelas;
using Hub.Monetrik.Api.Responses.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Despesas;

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
                ValorTotal = despesa.ValorTotal,
                TotalParcelas = despesa.TotalParcelas,
                DataRegistro = despesa.DataRegistro.ToString("dd/MM/yyyy"),
                Parcelas = ParcelasMapper.MapList(despesa.Parcelas.ToList())
            };
        }
    }
}