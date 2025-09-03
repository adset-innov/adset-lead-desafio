namespace AdSet.Lead.Core.Exceptions;

public class StringValidationException(string message) : Exception(message)
{
    public static void When(bool hasError, string message)
    {
        if (hasError)
            throw new StringValidationException(message);
    }
}