using FluentValidation;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Helpers.Messages;

namespace NS.SalesProduct.Business.Validations
{
    public class SaleValidation : Validation<Sale>
    {
        protected override void Validate()
        {
            RuleFor(sale => sale.CustomerId)
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Cliente vinculado"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Cliente vinculado"));

            RuleFor(sale => sale.TotalPrice)
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Preço total"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Preço total"))
                .GreaterThan(0).WithMessage(TriggerMessage.TriggerMessageGreaterThan("Preço total", 0));

            RuleFor(sale => sale.PaymentMethod)
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Forma de pagamento"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Forma de pagamento"));


            RuleFor(sale => sale.PricePaid)
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Preço pago"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Preço pago"))
                .GreaterThan(0).WithMessage(TriggerMessage.TriggerMessageGreaterThan("Preço pago", 0));

            RuleFor(sale => sale.PricePaid)
                .LessThan(101).WithMessage(TriggerMessage.TriggerMessageLessThan("Desconto", 0));

            RuleFor(sale => sale.SaleItems)
                .Must(saleItems => saleItems.Count > 0).WithMessage(TriggerMessage.TriggerMessageListCount("Produtos"));

            RuleForEach(sale => sale.SaleItems)
                .SetValidator(new SaleItemValidation());
        }
    }
}
