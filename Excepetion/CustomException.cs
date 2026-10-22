namespace SistemaEstoque.Exceptions;

public class CustomExcepetion : Exception
    {
    public int StatusCode { get; }

    public CustomExcepetion(string message, int statusCode)
    : base(message)
        {
        StatusCode = statusCode;
        }
    }