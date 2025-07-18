using Hub.Monetrik.Domain.Models.Entities.Despesas;
namespace Hub.Monetrik.Domain.Interfaces.Despesas
{
    public interface IDespesas
    {
        Task<List<IGrouping<string, Despesa>>> GetDespesasRepository();
        Task<Despesa> GetDespesaPorIdRepository(int id);  
    }
}