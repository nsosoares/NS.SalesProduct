using FluentValidation;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Helpers.Messages;

namespace NS.SalesProduct.Business.Validations
{
    public class CustomerValidation : Validation<Customer>
    {
        protected override void Validate()
        {
            RuleFor(customer => customer.Name)
                .MaximumLength(150).WithMessage(TriggerMessage.TriggerMessageMaxLength("Nome", 150))
                .MinimumLength(3).WithMessage(TriggerMessage.TriggerMessageMinLength("Nome", 3))
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Nome"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Nome"));
            RuleFor(customer => customer.Cpf)
                .MaximumLength(14).WithMessage(TriggerMessage.TriggerMessageMaxLength("CPF", 14))
                .MinimumLength(11).WithMessage(TriggerMessage.TriggerMessageMinLength("CPF", 11))
                .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("CPF"))
                .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("CPF"));
            RuleFor(customer => customer.BirthDate)
             .NotEmpty().WithMessage(TriggerMessage.TriggerMessageRequired("Data de nascimento"))
             .NotNull().WithMessage(TriggerMessage.TriggerMessageRequired("Data de nascimento"));
        }
    }
}
