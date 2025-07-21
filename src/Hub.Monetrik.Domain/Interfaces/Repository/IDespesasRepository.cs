using Hub.Monetrik.Domain.Models.Entities.Despesas;

namespace Hub.Monetrik.Domain.Interfaces.Repository
{
    public interface IDespesasRepository
    {
        Task<Despesa> CadastrarDespesasRepository(Despesa despesa);
        Task<List<Despesa>> BuscarDespesasRepository();
        Task<Despesa> BuscarDespesaPorIdRepository(int id);
        Task<Despesa> AtualizarSituacaoDespesaRepository(Despesa despesa);
    }
}