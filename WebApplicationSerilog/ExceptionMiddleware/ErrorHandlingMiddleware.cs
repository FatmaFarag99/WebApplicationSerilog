namespace WebApplicationSerilog.ExceptionMiddleware;

using System.Net;
using WebApplicationSerilog.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                ValidationException e => (int)HttpStatusCode.BadRequest,// custom application error
                NotFoundException e => (int)HttpStatusCode.NotFound,// not found error
                _ => (int)HttpStatusCode.InternalServerError,// unhandled error
            };
            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}


    //public static Task HandleExcepyion(HttpContext httpContext , Exception ex)
    //{
    //       HttpStatusCode code = HttpStatusCode.InternalServerError;

    //	string result = JsonConvert.SerializeObject(new { error = ex.Message });

    //	httpContext.Response.ContentType = "application/json";
    //	httpContext.Response.StatusCode = (int)code;

    //	return httpContext.Response.WriteAsync(result);
    //}
