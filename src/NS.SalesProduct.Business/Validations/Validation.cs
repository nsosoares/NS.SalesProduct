using FluentValidation;
using FluentValidation.Results;
using NS.SalesProduct.Business.Models;

namespace NS.SalesProduct.Business.Validations
{
    public abstract class Validation<TEntity> : AbstractValidator<TEntity> where TEntity : Entity
    {
        protected Validation()
        {
            ValidationResult = new ValidationResult();
        }

        public ValidationResult ValidationResult { get; private set; }

        public async Task<bool> IsValidAsync(TEntity entity)
        {
            Validate();
            ValidationResult = await ValidateAsync(entity);
            return ValidationResult.IsValid;
        }

        public bool IsValid(TEntity entity)
        {
            Validate();
            ValidationResult = Validate(entity);
            return ValidationResult.IsValid;
        }

        protected abstract void Validate();
    }
}
