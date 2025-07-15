using Hub.Monetrik.Domain.Models.Entities.Despesa;

namespace Hub.Monetrik.Domain.Interfaces.Repository
{
    public interface IDespesasRepository
    {
        Task CadastrarDespesasRepository(Despesa despesa);
    }
}