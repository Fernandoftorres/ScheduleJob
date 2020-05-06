using FluentValidation;
using FluentValidation.Results;

namespace ScheduleJOB.Domain
{
    public abstract class BaseDomain<T> : AbstractValidator<T> where T : BaseDomain<T>
    {
        protected BaseDomain()
        {
            ValidationResult = new ValidationResult();
        }

        public int Id { get; protected set; }

        public abstract bool EhValido();

        public ValidationResult ValidationResult { get; protected set; }
    }
}
