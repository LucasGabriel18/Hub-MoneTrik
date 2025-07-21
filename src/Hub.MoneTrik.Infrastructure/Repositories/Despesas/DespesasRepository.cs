using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesas;
using Hub.MoneTrik.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Hub.MoneTrik.Infrastructure.Repositories.Despesas
{    
    public class DespesasRepository : IDespesasRepository
    {
        private readonly HubMonetrikContext _context;
        public DespesasRepository(HubMonetrikContext context)
        {
            _context = context;
        }

        public async Task<Despesa> AtualizarSituacaoDespesaRepository(Despesa despesa)
        {
            _context.Update(despesa);
            await _context.SaveChangesAsync();
            return despesa;
        }
        public async Task<Despesa> BuscarDespesaPorIdRepository(int id)
        {
            return await _context.Despesas
                .AsNoTracking()
                .Include(d => d.Parcelas)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Despesa>> BuscarDespesasRepository()
        {
            return await _context.Despesas
                .AsNoTracking()
                .Include(d => d.Parcelas)
                .ToListAsync();
        }
        public async Task<Despesa> CadastrarDespesasRepository(Despesa despesa)
        {
            await _context.AddAsync(despesa);
            await _context.SaveChangesAsync();
            
            return despesa;
        }
    }
}