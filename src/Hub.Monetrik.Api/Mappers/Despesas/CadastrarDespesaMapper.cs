using Hub.Monetrik.Api.Responses.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Despesa;

namespace Hub.Monetrik.Api.Mappers.Despesas
{
    public class CadastrarDespesaMapper
    {
        public static CadastrarDespesaResponse Map(Despesa request)
        {
            var valorParcelaArredondado = Math.Round(request.ValorParcela, 2);

            return new CadastrarDespesaResponse
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Categoria = request.Categoria.ToString(),
                Tipo = request.Tipo.ToString(),
                TotalDeParcelas = request.TotalParcelas,
                NumeroParcela = request.NumeroParcela,
                ValorDaParcela = valorParcelaArredondado,
                ValorTotal = request.ValorTotal,
                DataPagamento = request.DataInicioPagamento.ToString("dd/MM/yyyy"),
                Situacao = request.Situacao.ToString()
            };
        }
    }
}