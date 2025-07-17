using Hub.Monetrik.Domain.Enums.Despesas;
using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

namespace Hub.Monetrik.Domain.Commands.Despesas.Atualizar
{
    public class AtualizarParcelaIndividualCommandHandler : IRequestHandler<AtualizarParcelaIndividualCommand, List<Despesa>>
    {
        private readonly IMediator _mediator;
        private readonly IDespesasRepository _despesasRepository;
        public AtualizarParcelaIndividualCommandHandler(IMediator mediator, IDespesasRepository repository)
        {
            _mediator = mediator;
            _despesasRepository = repository;
        }
        public async Task<List<Despesa>> Handle(AtualizarParcelaIndividualCommand request)
        {
            var listaDeDespesas = new List<Despesa>();
            try
            {
                var parcelaAtual = await _despesasRepository.BuscarDespesaPorIdRepository(request.Id);

                if (parcelaAtual == null)
                {
                    await _mediator.Publish(new Notification(
                        $"Parcela não encontrada.",
                        ENotificationType.Error));
                    return null;
                }

                if (request.AlterarTodasParcelasFuturas)
                {
                    // Determinar se devemos alterar todas as parcelas futuras

                    var parcelasFuturas = await _despesasRepository.BuscarParcelasFuturasPorTituloRepository(parcelaAtual.Titulo);
                    foreach (var parcela in parcelasFuturas)
                    {
                        var despesaAtualizada = await ValidaDespesas(parcela, request);
                        listaDeDespesas.Add(despesaAtualizada);
                    }
                    return listaDeDespesas;
                }
                else
                {
                    // Alterar apenas a parcela atual
                    var despesaAtualizada = await ValidaDespesas(parcelaAtual, request);
                    listaDeDespesas.Add(despesaAtualizada);

                    return listaDeDespesas;
                }
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new Notification(
                    $"Erro ao atualizar parcela da despesa: {ex.Message}",
                    ENotificationType.Error));

                return null;
            }
        }
        private async Task<Despesa> ValidaDespesas(Despesa parcela, AtualizarParcelaIndividualCommand request)
        {            
            if (parcela.Situacao != ESituacaoDespesa.Pago.ToString())
            {
                parcela.ValorParcela = request.NovoValorParcela;
                parcela = await _despesasRepository.AtualizarValorIndividualParcelaRepository(parcela);            
                return parcela;
            }
            else
            {
                await _mediator.Publish(new Notification(
                    $"Parcelas pagas não podem ter o valor modificado!",
                    ENotificationType.Error));
                return null;
            }
        }            
    }
}