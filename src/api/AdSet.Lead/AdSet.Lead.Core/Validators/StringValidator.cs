using AdSet.Lead.Core.Exceptions;

namespace AdSet.Lead.Core.Validators;

public static partial class StringValidator
{
    public static string Validate(
        string value,
        string paramName,
        int minLength,
        int maxLength
    )
    {
        var sanitizedValue = value.Trim();
        StringValidationException.When(
            string.IsNullOrEmpty(sanitizedValue),
            $"Invalid {paramName}. {paramName} is required."
        );
        StringValidationException.When(
            string.IsNullOrWhiteSpace(sanitizedValue),
            $"Invalid {paramName}. {paramName} is required."
        );
        StringValidationException.When(
            sanitizedValue.Length < minLength,
            $"Invalid {paramName}. {paramName} must have at least {minLength} characters."
        );
        StringValidationException.When(
            sanitizedValue.Length > maxLength,
            $"Invalid {paramName}. {paramName} must have {maxLength} characters or less."
        );

        return sanitizedValue;
    }
}