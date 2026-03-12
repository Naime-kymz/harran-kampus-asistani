using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace HarranKampusAsistani.API.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string HeaderName = "x-api-key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration config)
    {
        // Swagger'ı API key kontrolünden muaf tut
        var path = context.Request.Path.Value ?? "";
        if (path.StartsWith("/swagger"))
        {
            await _next(context);
            return;
        }

        // GET ve diğerleri serbest (sadece yazma işlemlerini koruyoruz)
        var method = context.Request.Method;
        var isWrite =
            HttpMethods.IsPost(method) ||
            HttpMethods.IsPut(method) ||
            HttpMethods.IsDelete(method) ||
            HttpMethods.IsPatch(method);

        if (!isWrite)
        {
            await _next(context);
            return;
        }

        // API Key kontrolü
        var expected = config["AdminApiKey"];
        if (string.IsNullOrWhiteSpace(expected))
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("AdminApiKey ayarlı değil.");
            return;
        }

        if (!context.Request.Headers.TryGetValue(HeaderName, out var provided) || provided != expected)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Gecersiz API Key.");
            return;
        }

        await _next(context);
    }
}