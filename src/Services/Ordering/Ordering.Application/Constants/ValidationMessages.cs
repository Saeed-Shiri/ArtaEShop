
namespace Ordering.Application.Constants;
public static class ValidationMessages
{
    public static string IsRequired(string property) => $"{property} is required.";
    public static string MaximumLength(string property, int length) => $"{property} must not be exceed {length} charachters.";
    public static string PositiveNumber(string property) => $"{property} must not be -ve.";
    public static string IsEmail(string property) => $"{property} must be entered in the format of an email.";
}
