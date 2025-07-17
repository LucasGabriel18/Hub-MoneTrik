using FluentValidation;

namespace Hub.Monetrik.Domain.Commands.Despesas.Atualizar
{
    public class AtualizarParcelaIndividualValidator : AbstractValidator<AtualizarParcelaIndividualCommand>
    {
        public AtualizarParcelaIndividualValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O Id digitado não pode ser vazio!")
                .GreaterThan(0)
                .WithMessage("O Id deve ser maior que zero!");

            RuleFor(x => x.NovoValorParcela)
                .NotEmpty()
                .WithMessage("O valor da parcela não pode ser vazio!")
                .GreaterThan(0)
                .WithMessage("O valor da parcela tem que ser maior que '0'!");
            When(x => x.NovoValorParcela > 0, () => {
                RuleFor(x => x.NovoValorParcela)
                    .Must(v => decimal.Round(v, 2) == v)
                    .WithMessage("O valor da parcela não pode ter mais de 2 casas decimais.");
            });
        }
    }
}