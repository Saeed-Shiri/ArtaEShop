

using FluentValidation.Results;

namespace Ordering.Application.Exceptions;
public class AppValidationException : ApplicationException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }
    public AppValidationException() : base("One or more validation error(s) occured.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public AppValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(x => x.Key, x => x.ToArray());
    }
}
