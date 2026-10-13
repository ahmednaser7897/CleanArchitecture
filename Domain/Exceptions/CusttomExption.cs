namespace Domain.Exceptions;

public class CusttomExption : Exception
{
    public int StatusCode { get; set; }
    public CusttomExption(string? message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
