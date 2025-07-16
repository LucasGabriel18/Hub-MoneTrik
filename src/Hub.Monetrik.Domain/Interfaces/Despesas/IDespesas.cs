using Hub.Monetrik.Domain.Models.Entities.Despesa;
namespace Hub.Monetrik.Domain.Interfaces.Despesas
{
    public interface IDespesas
    {
        Task<List<IGrouping<string, Despesa>>> GetDespesasRepository();
        Task<Despesa> GetDespesaPorIdRepository(int id);  
    }
}