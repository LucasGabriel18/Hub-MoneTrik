using Hub.Monetrik.Domain.Models.Entities.Despesa;
using static Hub.Monetrik.Mediator.Interfaces.IRequestTResponse;

namespace Hub.Monetrik.Domain.Commands.Despesas.Atualizar
{
    public class AtualizarSituacaoDespesaCommand : IRequest<Despesa>
    {
        public int Id { get; set; }
        public string NovaSituacao { get; set; }
    }
}