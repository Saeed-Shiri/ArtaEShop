

using FluentValidation.Results;

namespace Ordering.Application.Exceptions;
public class ValidationException : ApplicationException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }
    public ValidationException() : base("One or more validation error(s) occured.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(x => x.Key, x => x.ToArray());
    }
}
