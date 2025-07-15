using Hub.Monetrik.Domain.Enums.Despesas;
using Hub.Monetrik.Domain.Models.Entities.Despesa;
using static Hub.Monetrik.Mediator.Interfaces.IRequestTResponse;

namespace Hub.Monetrik.Domain.Commands.Despesas.Cadastrar
{
    public class CadastrarDespesasCommand : IRequest<Despesa>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public ECategoriaDespesas Categoria { get; set; }
        public ETipoDespesas Tipo { get; set; }
        public int Parcelas { get; set; } = 1; // Default 1 parcela
        public decimal ValorTotal { get; set; }             
        public DateTime DataInicioPagamento { get; set; }
    }
}