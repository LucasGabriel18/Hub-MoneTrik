using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using Hub.MoneTrik.Infrastructure.Enums.Despesas;
using Hub.MoneTrik.Infrastructure.Enums.Notifications;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

namespace Hub.Monetrik.Domain.Commands.Despesas.Cadastrar
{
    public class CadastrarDespesasCommandHandler : IRequestHandler<CadastrarDespesasCommand, Despesa>
    {
        private readonly IMediator _mediator;
        public CadastrarDespesasCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Despesa> Handle(CadastrarDespesasCommand request)
        {
            try
            {
                var valorParcela = request.ValorTotal / request.Parcelas;
                var dataPagamento = request.DataInicioPagamento;
                var despesas = new List<Despesa>();

                for (int i = 1; i <= request.Parcelas; i++)
                {
                    var despesa = new Despesa
                    {
                        // ID será gerado pelo banco (auto-increment)
                        Titulo = request.Titulo,
                        Descricao = request.Descricao,
                        Categoria = request.Categoria,
                        Tipo = request.Tipo,
                        ValorTotal = request.ValorTotal,
                        ValorParcela = valorParcela,
                        NumeroParcela = i,
                        TotalParcelas = request.Parcelas,
                        DataInicioPagamento = dataPagamento,
                        DataRegistro = DateTime.Now,
                        Situacao = ESituacaoDespesa.Pendente
                    };

                    despesas.Add(despesa);
                    
                    // Incrementa a data para próxima parcela
                    if (request.Tipo == ETipoDespesas.Fixa)
                    {
                        dataPagamento = dataPagamento.AddMonths(1);
                    }
                }

                await _mediator.Publish(new Notification(
                    "Despesa(s) cadastrada(s) com sucesso!",
                    ENotificationType.Information));

                return despesas.First();
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new Notification(
                    $"Erro ao cadastrar despesa: {ex.Message}",
                    ENotificationType.Error));

                return null;
            }
        }
    }
}