using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Parcelas;
using Hub.MoneTrik.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Hub.MoneTrik.Infrastructure.Repositories.Parcelas
{
    public class ParcelasRepository : IParcelasRepository
    {
        private readonly HubMonetrikContext _context;

        public ParcelasRepository(HubMonetrikContext context)
        {
            _context = context;
        }
        public async Task CadastrarParcelaRepository(Parcela parcela)
        {
            await _context.AddAsync(parcela);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Parcela>> BuscarParcelasPorDespesaIdRepository(int despesaId)
        {
            return await _context.Parcelas
                .Where(p => p.DespesaId == despesaId)
                .OrderBy(p => p.NumeroParcela)
                .ToListAsync();
        }

        public async Task<Parcela> BuscarParcelaPorIdRepository(int id)
        {
            return await _context.Parcelas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Parcela> AtualizarSituacaoParcelaRepository(Parcela parcela)
        {
            _context.Update(parcela);
            await _context.SaveChangesAsync();
            return parcela;
        }
    }
}