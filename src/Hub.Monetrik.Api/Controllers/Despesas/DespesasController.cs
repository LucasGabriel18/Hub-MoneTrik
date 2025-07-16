using Hub.Monetrik.Api.Mappers.Despesas;
using Hub.Monetrik.Domain.Commands.Despesas.Cadastrar;
using Hub.Monetrik.Domain.Interfaces.Despesas;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Microsoft.AspNetCore.Mvc;
namespace Hub.Monetrik.Api.Controllers.Despesas
{
    [ApiController]
    [Route("[controller]")]
    public class DespesasController : Controller
    {
        private readonly IMediator _mediator;
        private readonly NotificationHandler _notifications;
        private readonly IDespesas _despesasService;
        public DespesasController(
            IMediator mediator,
            NotificationHandler notifications,
            IDespesas despesas)
        {
            _mediator = mediator;
            _notifications = notifications;
            _despesasService = despesas;
        }

        [HttpPost("cadastrar-despesa")]
        public async Task<IActionResult> CadastrarDespesa([FromQuery] CadastrarDespesasCommand request)
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

            var response = CadastrarDespesaMapper.Map(result);
            return Created(string.Empty, new { success = true, data = response });
        }

        [HttpGet("buscar-despesas")]
        public async Task<IActionResult> BuscarDespesas()
        {
            var request = await _despesasService.GetDespesasRepository();            

            if (request is null)
            {
                var notifications = _notifications.GetNotifications().ToList();
                if (!notifications.Any())
                {
                    await _mediator.Publish(new Notification(
                        "Erro ao buscar despesas",
                        ENotificationType.Error));
                    notifications = _notifications.GetNotifications().ToList();
                }

                return BadRequest(new
                {
                    success = false,
                    errors = notifications.Select(n => new
                    {
                        message = n.Message,
                        type = n.Type.ToString()
                    })
                });
            }

            var response = BuscarDespesasMapper.Map(request);
            return Ok(new { success = true, data = response });
        }

        [HttpGet("buscar-despesa-por-id")]
        public async Task<IActionResult> BuscarDespesaPorId([FromQuery] int id)
        {
            var request = await _despesasService.GetDespesaPorIdRepository(id);
            
            if (request is null)
            {
                var notifications = _notifications.GetNotifications().ToList();
                if (!notifications.Any())
                {
                    await _mediator.Publish(new Notification(
                        $"Erro ao buscar despesa com ID {id}",
                        ENotificationType.Error));
                    notifications = _notifications.GetNotifications().ToList();
                }

                return BadRequest(new
                {
                    success = false,
                    errors = notifications.Select(n => new
                    {
                        message = n.Message,
                        type = n.Type.ToString()
                    })
                });
            }

            var response = BuscarDespesaPorIdMapper.Map(request);
            return Ok(new { success = true, data = response });
        }    

        [HttpPut("atualizar-situacao-despesa")]
        public async Task<IActionResult> AtualizarSituacaoDespesa([FromQuery] int id)
        {
            
            return Ok();
        }
    }
}