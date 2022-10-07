using BidvestMobilitySurveyBackendServer.Models;
using System.Net;

public class HttpResponseErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpResponseErrorHandlerMiddleware> logger;

    public HttpResponseErrorHandlerMiddleware(RequestDelegate next, ILogger<HttpResponseErrorHandlerMiddleware> logger)
    {
        _next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);

        }
        catch (Exception error)
        {
            logger.LogError(error.Message);
            logger.LogError(error.StackTrace);

            Response response;
            if (error is HttpResponseException responseError)
                response = responseError.Response;
            else
                response = new Response(HttpStatusCode.InternalServerError, error: "Something went wrong!");

            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
