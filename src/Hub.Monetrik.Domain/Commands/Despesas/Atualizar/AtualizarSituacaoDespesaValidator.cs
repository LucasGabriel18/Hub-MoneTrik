using FluentValidation;
using Hub.Monetrik.Domain.Enums.Despesas;

namespace Hub.Monetrik.Domain.Commands.Despesas.Atualizar
{
    public class AtualizarSituacaoDespesaValidator : AbstractValidator<AtualizarSituacaoDespesaCommand>
    {
        public AtualizarSituacaoDespesaValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID informado não pode ser vazio!");

            RuleFor(x => x.NovaSituacao)
                .NotEmpty()
                .WithMessage("A nova situação não pode ser vazia!")
                .IsEnumName(typeof(ESituacaoDespesa), caseSensitive: false)
                .WithMessage("A situação deve ser 'Pago' ou 'Pendente'.");
        }
    }
}