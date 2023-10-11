namespace ExceptionLibrary.Exceptions;

public class ClaimNotFoundException : ClientInputException
{
    public ClaimNotFoundException(string claimName)
        : base($"Claim with name = '{claimName}' not found.")
    {
    }
}