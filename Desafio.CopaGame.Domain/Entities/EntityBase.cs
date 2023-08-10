using FluentValidation.Results;
using System.Collections.Generic;

namespace Desafio.CopaGame.Domain.Entities
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            ValidationResult = new ValidationResult();
        }
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool IsValid();
        public ICollection<ValidationFailure> GetInvalidDataError()
        {
            return ValidationResult.Errors;
        }

    }
}
