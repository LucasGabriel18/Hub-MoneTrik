using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hub.Monetrik.Domain.Models.Entities.Parcelas;
using static Hub.Monetrik.Mediator.Interfaces.IRequestTResponse;

namespace Hub.Monetrik.Domain.Commands.Parcelas.Atualizar
{
    public class AtualizarSituacaoParcelaCommand : IRequest<Parcela>
    {
        public int Id { get; set; }
        public string NovaSituacao { get; set; }
    }
}