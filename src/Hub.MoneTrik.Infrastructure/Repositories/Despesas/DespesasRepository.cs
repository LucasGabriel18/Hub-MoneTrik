using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
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

        public async Task<Despesa> AtualizarValorIndividualParcelaRepository(Despesa despesa)
        {
            _context.Update(despesa);
            await _context.SaveChangesAsync();
            return despesa;
        }

        public async Task<Despesa> BuscarDespesaPorIdRepository(int id)
        {
            return await _context.Despesas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Despesa>> BuscarDespesasRepository()
        {
            return await _context.Despesas.ToListAsync();;
        }

        public async Task<List<Despesa>> BuscarParcelasFuturasPorTituloRepository(string titulo)
        {
            return await _context.Despesas
                .Where(d => d.Titulo == titulo)
                .ToListAsync();            
        }

        public async Task CadastrarDespesasRepository(Despesa despesa)
        {
            await _context.AddAsync(despesa);
            await _context.SaveChangesAsync();
        }
    }
}