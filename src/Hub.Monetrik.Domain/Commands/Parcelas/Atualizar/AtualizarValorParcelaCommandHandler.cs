using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Parcelas;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

namespace Hub.Monetrik.Domain.Commands.Parcelas.Atualizar
{
    public class AtualizarValorParcelaCommandHandler : IRequestHandler<AtualizarValorParcelaCommand, Parcela>
    {
        private readonly IMediator _mediator;
        private readonly IParcelasRepository _parcelasRepository;
        private readonly IDespesasRepository _despesasRepository;
        public AtualizarValorParcelaCommandHandler(IParcelasRepository repositoryParcela, IDespesasRepository repositoryDespesa, IMediator mediator)
        {
            _parcelasRepository = repositoryParcela;
            _despesasRepository = repositoryDespesa;
            _mediator = mediator;     
        }
        public async Task<Parcela> Handle(AtualizarValorParcelaCommand request)
        {
            try
            {
                var parcela = await _parcelasRepository.BuscarParcelaPorIdRepository(request.IdParcela);
                var despesa = await _despesasRepository.BuscarDespesaPorIdRepository(parcela.DespesaId);

                if (parcela is null || despesa is null)
                {
                    await _mediator.Publish(new Notification(
                            $"Não encontrado esta despesa!",
                            ENotificationType.Error));
                    return null;
                }
                
                if (parcela.Situacao.ToString() == "Pago")
                {
                    await _mediator.Publish(new Notification(
                            $"Não é possível editar o valor de uma parcela já paga!",
                            ENotificationType.Error));
                    return null;
                }

                var diferenca = request.NovoValor - parcela.ValorParcela;                    
                despesa.ValorTotal += diferenca;
                parcela.ValorParcela = request.NovoValor;

                var despesaAtualizada = await _despesasRepository.AtualizarValorTotalDespesaRepository(despesa);
                var parcelaAtualizada = await _parcelasRepository.AtualizarValorParcelaParcelaRepository(parcela);
                return parcelaAtualizada;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new Notification(
                    $"Erro ao atualizar o valor da parcela: {ex.Message}",
                    ENotificationType.Error));
                return null;
            }            
        }
    }
}