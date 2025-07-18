using Hub.Monetrik.Api.Mappers.Parcelas;
using Hub.Monetrik.Api.Responses.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Despesas;

namespace Hub.Monetrik.Api.Mappers.Despesas
{
    public class CadastrarDespesaMapper
    {
        public static CadastrarDespesaResponse Map(Despesa despesa) 
        {
            return new CadastrarDespesaResponse
            {
                Id = despesa.Id,
                Titulo = despesa.Titulo,
                Descricao = despesa.Descricao,
                Categoria = despesa.Categoria,
                Tipo = despesa.Tipo,
                TotalParcelas = despesa.TotalParcelas,
                ValorTotal = despesa.ValorTotal,
                Parcelas = ParcelasMapper.MapList(despesa.Parcelas.ToList())
            };
        }
    }
}