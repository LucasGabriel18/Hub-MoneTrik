using FluentValidation;

namespace Hub.Monetrik.Domain.Commands.Parcelas.Atualizar
{
    public class AtualizarValorParcelaValidator : AbstractValidator<AtualizarValorParcelaCommand>
    {
        public AtualizarValorParcelaValidator()
        {
            RuleFor(x => x.IdParcela)
                .NotEmpty()
                .WithMessage("O valor de id não pode ser vazio");
                
            RuleFor(x => x.NovoValor)
                .NotEmpty()
                .WithMessage("O novo valor da parcela não pode ser vazio.")
                .GreaterThan(0)
                .WithMessage("O valor da parcela tem que ser maior que '0'.");
        }
    }
}