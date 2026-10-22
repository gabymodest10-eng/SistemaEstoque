using SistemaEstoque.Exceptions;

namespace SistemaEstoque.Middlewares;

public class ExceptionMiddlewares
    {
    private readonly RequestDelegate _next;

    public ExceptionMiddlewares(RequestDelegate next)
        {
        _next = next;
        }

    public async Task Invoke(HttpContext context)
        {
        try
            {
            await _next(context);
            }
        catch (CustomExcepetion ex)
            {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsync(ex.Message);
            
            }
        }
    }




