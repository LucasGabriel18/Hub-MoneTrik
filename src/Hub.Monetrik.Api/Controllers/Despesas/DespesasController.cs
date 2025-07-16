using Hub.Monetrik.Api.Mappers.Despesas;
using Hub.Monetrik.Domain.Commands.Despesas.Cadastrar;
using Hub.Monetrik.Domain.Interfaces.Despesas;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Microsoft.AspNetCore.Mvc;
namespace Hub.Monetrik.Api.Controllers.Despesas
{
    [ApiController]
    [Route("[controller]")]
    public class DespesasController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IDespesas _despesasService;
        private readonly NotificationHandler _notifications;
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
            var response = await _despesasService.GetDespesasRepository();

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

            return Ok(new { success = true, data = response });            
        }
    }
}