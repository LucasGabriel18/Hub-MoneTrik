using System.ComponentModel.DataAnnotations;
using Hub.Monetrik.Api.Mappers.Despesas;
using Hub.Monetrik.Domain.Commands.Despesas.Cadastrar;
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
        private readonly NotificationHandler _notifications;
        public DespesasController(
            IMediator mediator,
            NotificationHandler notifications)
        {
            _mediator = mediator;
            _notifications = notifications;
        }

        [HttpPost("/cadastrar-despesa")]
        public async Task<IActionResult> CadastrarDespesas([FromBody] CadastrarDespesasCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (_notifications.HasNotifications())
                {
                    return BadRequest(new
                    {
                        success = false,
                        errors = _notifications.GetNotifications().Select(n => n.Message).ToList()
                    });
                }

                var response = CadastrarDespesaMapper.Map(result);

                return Created(string.Empty, new
                {
                    success = true,
                    data = response
                });
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = _notifications.GetNotifications().Select(n => n.Message).ToList()
                });
            }
        }
    }
}