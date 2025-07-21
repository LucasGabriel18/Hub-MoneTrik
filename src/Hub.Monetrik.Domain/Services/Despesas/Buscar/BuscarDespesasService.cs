using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Despesas;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesas;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
namespace Hub.Monetrik.Domain.Services.Despesas.Buscar
{
    public class BuscarDespesasService : IDespesas
    {
        private readonly IDespesasRepository _despesasRepository;
        private readonly IMediator _mediator;
        public BuscarDespesasService(IDespesasRepository despesas, IMediator mediator)
        {
            _despesasRepository = despesas;
            _mediator = mediator;
        }

        public async Task<Despesa> GetDespesaPorIdRepository(int id)
        {
            try
            {
                var response = await _despesasRepository.BuscarDespesaPorIdRepository(id);

                if (response is null)
                {
                    var notification = new Notification(
                        $"Despesa para este ID '{id}' não encontrada!",
                        ENotificationType.Error);
                    
                    await _mediator.Publish(notification).ConfigureAwait(false);
                    await Task.Delay(100); // Pequeno delay para garantir que a notificação seja processada
                    return null;
                }
                
                await _mediator.Publish(new Notification(
                    "Despesa encontrada com sucesso!",
                    ENotificationType.Information));
                
                return response;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new Notification(
                    $"Erro ao Agrupar Despesa(s): {ex.Message}",
                    ENotificationType.Error));
                return null;
            }

        }

        public async Task<List<IGrouping<string, Despesa>>> GetDespesasRepository()
        {            
            try
            {
                var response = await _despesasRepository.BuscarDespesasRepository();            
                if (response is null || !response.Any())
                {
                    await _mediator.Publish(new Notification(
                        "Nenhuma despesa encontrada!",
                        ENotificationType.Error));
                    return null;
                }
                var responseAgroupados = response.GroupBy(x => x.Titulo).ToList();
                
                await _mediator.Publish(new Notification(
                    "Despesas agrupadas com sucesso!",
                    ENotificationType.Information));
                
                return responseAgroupados;
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new Notification(
                    $"Erro ao agrupar Despesa(s): {ex.Message}",
                    ENotificationType.Error));
                return null;
            }        
        }
    }
}