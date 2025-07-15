using FluentValidation;
using Hub.Monetrik.Domain.Enums.Despesas;

namespace Hub.Monetrik.Domain.Commands.Despesas.Cadastrar
{
    public class CadastrarDespesasValidator : AbstractValidator<CadastrarDespesasCommand>
    {
        public CadastrarDespesasValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("O título é obrigatório")
                .MaximumLength(100)
                .WithMessage("O título não pode ter mais que 100 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("O campo descrição, não pode ser vazio!");

            RuleFor(x => x.Categoria)
                .IsInEnum()
                .WithMessage("Categoria inválida. Escolha entre: Internet, Água, Luz, Assinatura ou Lazer");

            RuleFor(t => t.Tipo)                
                .IsInEnum()
                .WithMessage("O valor digitado não corresponde ao esperado! Por favor escolha entre (Fixa ou Variavel)");

            RuleFor(x => x.Parcelas)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O número mínimo de parcelas é 1")
                .LessThanOrEqualTo(60)
                .WithMessage("O número máximo de parcelas é 60")
                .Must((command, parcelas) =>
                {
                    // Se for despesa variável, só pode ter 1 parcela
                    if (command.Tipo != ETipoDespesas.Fixa)
                        return parcelas == 1;

                    // Se for despesa fixa, pode ter até 60 parcelas
                    return true;
                })
                .WithMessage("Apenas despesas fixas podem ser parceladas");

            RuleFor(x => x.DataInicioPagamento)
                .NotEmpty()
                .WithMessage("A data de pagamento é obrigatória")
                .Must(data => data.Date >= DateTime.Now.Date)
                .WithMessage("A data de pagamento deve ser igual ou maior que hoje");

            RuleFor(x => x.ValorTotal)
                .NotEmpty()
                .WithMessage("O valor total é obrigatório")
                .GreaterThan(0)
                .WithMessage("O valor total deve ser maior que zero")
                .PrecisionScale(10, 2, false)
                .WithMessage("O valor total não pode ter mais que 2 casas decimais");
        }
    }
}