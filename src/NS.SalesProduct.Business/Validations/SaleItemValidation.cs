using FluentValidation;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Helpers.Messages;

namespace NS.SalesProduct.Business.Validations
{
    public class SaleItemValidation : Validation<SaleItem>
    {
        protected override void Validate()
        {
            RuleFor(saleItem => saleItem.ProductId)
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Produto selecionado"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Produto selecionado"));

            RuleFor(saleItem => saleItem.SaleId)
               .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Venda selecionada"))
               .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Venda selecionada"));

            RuleFor(saleItem => saleItem.Amount)
               .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Quantidade"))
               .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Quantidade"))
               .GreaterThan(0).WithMessage(TriggerMessage.TriggerMessageGreaterThan("Quantidade", 0));
        }
    }
}
