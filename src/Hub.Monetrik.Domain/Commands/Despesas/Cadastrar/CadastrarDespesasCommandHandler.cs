using Hub.Monetrik.Domain.Enums.Despesas;
using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

namespace Hub.Monetrik.Domain.Commands.Despesas.Cadastrar
{
    public class CadastrarDespesasCommandHandler : IRequestHandler<CadastrarDespesasCommand, Despesa>
    {
        private readonly IMediator _mediator;
        private readonly IDespesasRepository _despesasRepository;
        public CadastrarDespesasCommandHandler(IMediator mediator, IDespesasRepository repository)
        {
            _mediator = mediator;
            _despesasRepository = repository;
        }

        public async Task<Despesa> Handle(CadastrarDespesasCommand request)
        {
            try
            {
                var valorParcela = Math.Round(request.ValorTotal / request.Parcelas, 2);
                var dataPagamento = request.DataInicioPagamento;
                var despesas = new List<Despesa>();

                for (int i = 1; i <= request.Parcelas; i++)
                {
                    var despesa = new Despesa
                    {
                        // ID será gerado pelo banco (auto-increment)
                        Titulo = request.Titulo,
                        Descricao = request.Descricao,
                        Categoria = request.Categoria.ToString(),
                        Tipo = request.Tipo.ToString(),
                        ValorTotal = Math.Round(request.ValorTotal, 2),
                        ValorParcela = valorParcela,
                        NumeroParcela = i,
                        TotalParcelas = request.Parcelas,
                        DataInicioPagamento = dataPagamento,
                        DataRegistro = DateTime.Now,
                        Situacao = ESituacaoDespesa.Pendente.ToString()
                    };

                    despesas.Add(despesa);
                    
                    await _despesasRepository.CadastrarDespesasRepository(despesa);
                    
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