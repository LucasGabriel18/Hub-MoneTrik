using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Parcelas;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

namespace Hub.Monetrik.Domain.Commands.Parcelas.Atualizar
{
    public class AtualizarSituacaoParcelaCommandHandler : IRequestHandler<AtualizarSituacaoParcelaCommand, Parcela>
    {
        private readonly IMediator _mediator;
        private readonly IParcelasRepository _parcelasRepository;
        
        public AtualizarSituacaoParcelaCommandHandler(IMediator mediator, IParcelasRepository parcelasRepository)
        {
            _mediator = mediator;
            _parcelasRepository = parcelasRepository;
        }
        public async Task<Parcela> Handle(AtualizarSituacaoParcelaCommand request)
        {
            try
            {
                var parcela = await _parcelasRepository.BuscarParcelaPorIdRepository(request.Id);

                if (parcela == null)
                {
                    await _mediator.Publish(new Notification(
                        $"Parcela com ID {request.Id} não encontrada",
                        ENotificationType.Error));
                    return null;
                }
                
                if (parcela.Situacao == request.NovaSituacao)
                {
                    await _mediator.Publish(new Notification(
                        $"A situação desta parcela é igual a 'nova situação'!",
                        ENotificationType.Warning));
                    return null;
                }

                parcela.Situacao = request.NovaSituacao;
                var response = await _parcelasRepository.AtualizarSituacaoParcelaRepository(parcela);

                await _mediator.Publish(new Notification(
                    $"Situação da parcela {parcela.NumeroParcela} atualizada com sucesso!",
                    ENotificationType.Information));
                
                return response;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new Notification(
                    $"Erro ao atualizar situação da parcela: {ex.Message}",
                    ENotificationType.Error));
                return null;
            }
        }
    }
}