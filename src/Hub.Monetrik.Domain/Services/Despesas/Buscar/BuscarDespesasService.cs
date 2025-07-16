using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Despesas;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
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
        public async Task<List<IGrouping<string, Despesa>>> GetDespesasRepository()
        {            
            try
            {
                var response = await _despesasRepository.BuscarDespesasRepository();
                var responseAgroupados = response.GroupBy(x => x.Titulo).ToList();

                await _mediator.Publish(new Notification(
                    "Agrupado Despesa(s) com Sucesso!",
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