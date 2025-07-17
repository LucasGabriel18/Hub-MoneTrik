using Hub.Monetrik.Domain.Models.Entities.Despesa;
using static Hub.Monetrik.Mediator.Interfaces.IRequestTResponse;

namespace Hub.Monetrik.Domain.Commands.Despesas.Atualizar
{
    public class AtualizarParcelaIndividualCommand : IRequest<List<Despesa>>
    {
        public int Id { get; set; }
        public decimal NovoValorParcela { get; set; }
        public bool AlterarTodasParcelasFuturas { get; set; } = false; // Default é false (altera apenas a parcela atual)
    }
}