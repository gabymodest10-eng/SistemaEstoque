using FluentValidation;
using SistemaEstoque.Models;


namespace SistemaEstoque.Validators;

public class ProdutoValidator : AbstractValidator<Produto>
    {
    public ProdutoValidator()
        {
        RuleFor(x => x.Codigo)
           .GreaterThan(0);


        RuleFor(x => x.Nome)
         .NotEmpty();

        RuleFor(x => x.Quantidade)

          .GreaterThanOrEqualTo(0);
        }
    }