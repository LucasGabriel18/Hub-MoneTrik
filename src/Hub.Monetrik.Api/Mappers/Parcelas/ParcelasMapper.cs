using Hub.Monetrik.Api.Responses.Parcelas;
using Hub.Monetrik.Domain.Models.Entities.Parcelas;

namespace Hub.Monetrik.Api.Mappers.Parcelas
{
    public class ParcelasMapper
    {
        public static ParcelaResponse Map(Parcela parcela)
        {
            return new ParcelaResponse
            {
                Id = parcela.Id,
                DespesaId = parcela.DespesaId,
                NumeroParcela = parcela.NumeroParcela,
                ValorParcela = parcela.ValorParcela,
                DataVencimento = parcela.DataVencimento,
                Situacao = parcela.Situacao
            };
        }
        
        public static List<ParcelaResponse> MapList(List<Parcela> parcelas)
        {
            return parcelas.Select(Map).ToList();
        }
    }
}