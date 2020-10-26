using FluentValidation;
using ShoppingCart.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 250).WithMessage("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght} caracteres.");
            
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 500).WithMessage("O campo {PropertyName} precisa ter entre {MinLenght} e {MaxLenght} caracteres.");

            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(5).WithMessage("O campo {PropertyName} precisa ter 5 caracteres.");

            RuleFor(p => p.Imagem)
               .NotEmpty().WithMessage("Uma imagem precisa ser fornecida.");

            RuleFor(p => p.Preco)
                .NotNull()
                .GreaterThanOrEqualTo(0);



        }
    }
}
