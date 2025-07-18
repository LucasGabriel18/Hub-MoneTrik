using Hub.Monetrik.Domain.Enums.Despesas;
using Hub.Monetrik.Domain.Enums.Notifications;
using Hub.Monetrik.Domain.Interfaces.Repository;
using Hub.Monetrik.Domain.Models.Entities.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Parcelas;
using Hub.Monetrik.Domain.Notifications;
using Hub.Monetrik.Mediator.Interfaces.Mediator;
using static Hub.Monetrik.Mediator.Interfaces.Mediator.IRequestHandler;

namespace Hub.Monetrik.Domain.Commands.Despesas.Cadastrar
{
    public class CadastrarDespesasCommandHandler : IRequestHandler<CadastrarDespesasCommand, Despesa>
    {
        private readonly IMediator _mediator;
        private readonly IDespesasRepository _despesasRepository;
        private readonly IParcelasRepository _parcelasRepository;
        
        public CadastrarDespesasCommandHandler(
            IMediator mediator, 
            IDespesasRepository despesasRepository,
            IParcelasRepository parcelasRepository)
        {
            _mediator = mediator;
            _despesasRepository = despesasRepository;
            _parcelasRepository = parcelasRepository;
        }

        public async Task<Despesa> Handle(CadastrarDespesasCommand request)
        {
            try
            {
                var valorTotal = Math.Round(request.ValorParcela * request.QntdParcelas, 2);
                
                // Criar a despesa principal
                var despesa = new Despesa
                {
                    Titulo = request.Titulo,
                    Descricao = request.Descricao,
                    Categoria = request.Categoria.ToString(),
                    Tipo = request.Tipo.ToString(),
                    ValorTotal = valorTotal,
                    TotalParcelas = request.QntdParcelas,
                    DataRegistro = DateTime.Now
                };

                // Salvar a despesa principal para obter o ID
                var despesaSalva = await _despesasRepository.CadastrarDespesasRepository(despesa);
                
                // Criar as parcelas
                var dataPagamento = request.DataInicioPagamento;
                
                for (int i = 1; i <= request.QntdParcelas; i++)
                {
                    var parcela = new Parcela
                    {
                        DespesaId = despesaSalva.Id,
                        NumeroParcela = i,
                        ValorParcela = request.ValorParcela,
                        DataVencimento = dataPagamento.ToString("dd/MM/yyyy"),
                        Situacao = ESituacaoDespesa.Pendente.ToString()
                    };

                    await _parcelasRepository.CadastrarParcelaRepository(parcela);
                    
                    // Incrementa a data para próxima parcela
                    if (request.Tipo == ETipoDespesas.Fixa)
                    {
                        dataPagamento = dataPagamento.AddMonths(1);
                    }
                }

                // Carregar a despesa completa com suas parcelas
                var despesaCompleta = await _despesasRepository.BuscarDespesaPorIdRepository(despesaSalva.Id);

                await _mediator.Publish(new Notification(
                    "Despesa(s) cadastrada(s) com sucesso!",
                    ENotificationType.Information));
                
                return despesaCompleta;
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