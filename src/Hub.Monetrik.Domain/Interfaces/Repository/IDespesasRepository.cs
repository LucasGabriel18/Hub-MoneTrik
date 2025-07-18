using Hub.Monetrik.Domain.Models.Entities.Despesa;

namespace Hub.Monetrik.Domain.Interfaces.Repository
{
    public interface IDespesasRepository
    {
        Task CadastrarDespesasRepository(Despesa despesa);
        Task<List<Despesa>> BuscarDespesasRepository();
        Task<Despesa> BuscarDespesaPorIdRepository(int id);
        Task<Despesa> AtualizarSituacaoDespesaRepository(Despesa despesa);
        Task<Despesa> AtualizarValorIndividualParcelaRepository(Despesa despesa);
        Task<List<Despesa>> BuscarParcelasFuturasPorTituloRepository(string titulo);
        Task<Despesa> AtualizarValorTotalParcelasRepository(int id, decimal novoValorParcela);
    }
}