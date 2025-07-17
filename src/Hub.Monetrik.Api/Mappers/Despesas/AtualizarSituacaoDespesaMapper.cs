using Hub.Monetrik.Api.Responses.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Despesa;

namespace Hub.Monetrik.Api.Mappers.Despesas
{
    public class AtualizarSituacaoDespesaMapper
    {
        public static AtualizarSituacaoDespesaResponse Map(Despesa despesa)
        {
            return new AtualizarSituacaoDespesaResponse
            {
                Message = $"Atualizado a situação da despesa ID: [{despesa.Id}] - {despesa.Titulo} para '{despesa.Situacao}'. Parcela: [{despesa.NumeroParcela}º]!"
            };
        }        
    }
}