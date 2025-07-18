using Hub.Monetrik.Domain.Models.Entities.Parcelas;

namespace Hub.Monetrik.Domain.Interfaces.Repository
{
    public interface IParcelasRepository
    {
        Task CadastrarParcelaRepository(Parcela parcela);
        Task<List<Parcela>> BuscarParcelasPorDespesaIdRepository(int despesaId);
        Task<Parcela> BuscarParcelaPorIdRepository(int id);
        Task<Parcela> AtualizarSituacaoParcelaRepository(Parcela parcela);
    }
}