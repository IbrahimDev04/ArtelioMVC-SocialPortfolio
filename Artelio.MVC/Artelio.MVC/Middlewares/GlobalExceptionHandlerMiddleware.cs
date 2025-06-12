namespace Artelio.MVC.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    public readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }   catch (Exception ex)
        {
            string url = "/Error/ErrorPage";
            context.Response.Redirect(url);
        }
    }
}
