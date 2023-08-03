namespace Turbulence.API;

public class ApiException : Exception
{
    public ApiException(string message) : base(message)
    {
    }
}