using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

namespace Hub.Monetrik.Domain.Commands.Despesas.Atualizar
{
    public class AtualizarSituacaoDespesaCommandHandler : IRequestHandler<AtualizarSituacaoDespesaCommand, Despesa>
    {
        private readonly IMediator _mediator;
        private readonly IDespesasRepository _despesasRepository;
        public AtualizarSituacaoDespesaCommandHandler(IMediator mediator, IDespesasRepository repository)
        {
            _mediator = mediator;
            _despesasRepository = repository;
        }
        public async Task<Despesa> Handle(AtualizarSituacaoDespesaCommand request)
        {
            try
            {
                var despesa = await _despesasRepository.BuscarDespesaPorIdRepository(request.Id);

                if (despesa == null)
                {
                    await _mediator.Publish(new Notification(
                        $"Despesa com ID {request.Id} não encontrada",
                        ENotificationType.Error));
                    return null;
                }
                if (despesa.Situacao == request.NovaSituacao)
                {
                    await _mediator.Publish(new Notification(
                        $"A situação desta despesa é igual a 'nova situação'!",
                        ENotificationType.Warning));

                    return null;
                }

                despesa.Situacao = request.NovaSituacao;

                var response = await _despesasRepository.AtualizarSituacaoDespesa(despesa);

                return response;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new Notification(
                    $"Erro ao cadastrar despesa: {ex.Message}",
                    ENotificationType.Error));

                return null;
            }        
        }
    }
}