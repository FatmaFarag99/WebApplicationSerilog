namespace WebApplicationSerilog;

using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

public class ErrorHandlingMiddleware
{
	private readonly RequestDelegate next;

	public ErrorHandlingMiddleware(RequestDelegate next)
	{
		this.next = next;
	}

	public async Task Invoke(HttpContext httpContext)
	{
		try
		{
			await next(httpContext);
		}
		catch (Exception)
		{

			throw;
		}
	}
	// 
	public static Task HandleExcepyion(HttpContext httpContext , Exception ex)
	{
        HttpStatusCode code = HttpStatusCode.InternalServerError;

		string result = JsonConvert.SerializeObject(new { error = ex.Message });
		
		httpContext.Response.ContentType = "application/json";
		httpContext.Response.StatusCode = (int)code;

		return httpContext.Response.WriteAsync(result);
	}
}
