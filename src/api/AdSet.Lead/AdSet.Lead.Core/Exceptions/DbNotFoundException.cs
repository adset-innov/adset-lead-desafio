namespace AdSet.Lead.Core.Exceptions;

public class DbNotFoundException(string message) : Exception(message)
{
    public static void When(bool hasError, string message)
    {
        if (hasError)
            throw new DbNotFoundException(message);
    }
}