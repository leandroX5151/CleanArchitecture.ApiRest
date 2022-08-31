using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions
{
    public  class ValidationException: ApplicationException
    {
        public ValidationException(): base("Se presentaron uno o más errores de validación")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException( IEnumerable<ValidationFailure> failures): this()
        {
            Errors = failures.GroupBy(static e => e.PropertyName, static e => e.ErrorMessage)
                .ToDictionary(static failureGroup => failureGroup.Key, static failureGroup => failureGroup.ToArray());
        }


        public IDictionary<string, string[]> Errors { get;}
    }
}
