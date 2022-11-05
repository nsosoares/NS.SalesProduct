using FluentValidation;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Helpers.Messages;

namespace NS.SalesProduct.Business.Validations
{
    public class ProductValidation : Validation<Product>
    {
        protected override void Validate()
        {
            RuleFor(product => product.Name)
                .MaximumLength(150).WithMessage(TriggerMessage.TriggerMessageMaxLength("Nome", 150))
                .MinimumLength(3).WithMessage(TriggerMessage.TriggerMessageMinLength("Nome", 3))
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Nome"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Nome"));

            RuleFor(product => product.Price)
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Preço"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Preço"))
                .GreaterThan(0).WithMessage(TriggerMessage.TriggerMessageGreaterThan("Preço", 0));

            RuleFor(product => product.CostPrice)
               .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Preço de custo"))
               .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Preço de custo"))
               .GreaterThan(0).WithMessage(TriggerMessage.TriggerMessageGreaterThan("Preço de custo", 0))
               .LessThan(product => product.Price).WithMessage(product => TriggerMessage.TriggerMessageGreaterThan("Preço de custo", product.Price));
        }
    }
}
