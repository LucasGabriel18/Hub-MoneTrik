using Hub.Monetrik.Domain.Models.Entities.Parcelas;
using static Hub.Monetrik.Mediator.Interfaces.IRequestTResponse;

namespace Hub.Monetrik.Domain.Commands.Parcelas.Atualizar
{
    public class AtualizarValorParcelaCommand : IRequest<Parcela>
    {
        public int IdParcela { get; set; }
        public decimal NovoValor { get; set; }
    }
}