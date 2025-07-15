using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Hub.MoneTrik.Infrastructure.Context;

namespace Hub.MoneTrik.Infrastructure.Repositories.Despesas
{    
    public class DespesasRepository : IDespesasRepository
    {
        private readonly HubMonetrikContext _context;
        public DespesasRepository(HubMonetrikContext context)
        {
            _context = context;
        }
        public async Task CadastrarDespesasRepository(Despesa despesa)
        {
            await _context.AddAsync(despesa);
            await _context.SaveChangesAsync();
        }
    }
}