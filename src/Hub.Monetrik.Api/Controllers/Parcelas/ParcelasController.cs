using Hub.Monetrik.Api.Mappers.Parcelas;
using Hub.Monetrik.Domain.Commands.Parcelas.Atualizar;
using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Hub.Monetrik.Api.Controllers.Parcelas
{
    [ApiController]
    [Route("[controller]")]
    public class ParcelasController : Controller
    {
        private readonly IMediator _mediator;
        private readonly NotificationHandler _notifications;
        private readonly IParcelasRepository _parcelasRepository;

        public ParcelasController(
            IMediator mediator,
            NotificationHandler notifications,
            IParcelasRepository parcelasRepository)
        {
            _mediator = mediator;
            _notifications = notifications;
            _parcelasRepository = parcelasRepository;
        }

        [HttpGet("buscar-por-despesa/{despesaId}")]
        public async Task<IActionResult> BuscarParcelasPorDespesa(int despesaId)
        {
            var parcelas = await _parcelasRepository.BuscarParcelasPorDespesaIdRepository(despesaId);

            if (parcelas == null || !parcelas.Any())
            {
                await _mediator.Publish(new Notification(
                    $"Nenhuma parcela encontrada para a despesa com ID {despesaId}",
                    ENotificationType.Error));

                return BadRequest(new
                {
                    success = false,
                    errors = _notifications.GetNotifications().Select(n => new
                    {
                        message = n.Message,
                        type = n.Type.ToString()
                    })
                });
            }

            var response = ParcelasMapper.MapList(parcelas);
            return Ok(new { success = true, data = response });
        }
        
        [HttpPut("atualizar-situacao")]
        public async Task<IActionResult> AtualizarSituacaoParcela([FromQuery] AtualizarSituacaoParcelaCommand request)
        {
            var result = await _mediator.Send(request);
            
            if (_notifications.HasNotifications())
            {
                var errors = _notifications.GetNotifications();
                return BadRequest(new
                {
                    success = false,
                    errors = errors.Select(n => new
                    {
                        message = n.Message,
                        type = n.Type.ToString()
                    })
                });
            }
            
            var response = ParcelasMapper.Map(result);
            return Ok(new { success = true, data = response });
        }
    }
}