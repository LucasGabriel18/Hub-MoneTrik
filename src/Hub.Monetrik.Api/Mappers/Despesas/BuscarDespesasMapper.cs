using Hub.Monetrik.Api.Responses.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Despesas;
using System.Linq;

namespace Hub.Monetrik.Api.Mappers.Despesas
{
    public class BuscarDespesasMapper
    {
        public static BuscarDespesasResponse Map(List<IGrouping<string, Despesa>> despesasAgrupadas)
        {
            // Flatten a lista agrupada para obter todas as despesas
            var todasDespesas = despesasAgrupadas.SelectMany(grupo => grupo).ToList();
            
            return new BuscarDespesasResponse
            {
                Despesas = [.. todasDespesas.Select(d => new DespesaDTO
                {
                    Id = d.Id,
                    Titulo = d.Titulo,
                    Descricao = d.Descricao,
                    Categoria = d.Categoria,
                    Tipo = d.Tipo,
                    TotalParcelas = d.TotalParcelas,
                    ValorTotal = d.ValorTotal,
                    DataRegistro = d.DataRegistro,
                    Parcelas = [.. d.Parcelas.Select(p => new ParcelaDTO
                    {
                        Id = p.Id,
                        DespesaId = p.DespesaId,
                        NumeroParcela = p.NumeroParcela,
                        ValorParcela = p.ValorParcela,
                        DataVencimento = p.DataVencimento,
                        Situacao = p.Situacao
                    })]
                })]
            };
        }
    }
}