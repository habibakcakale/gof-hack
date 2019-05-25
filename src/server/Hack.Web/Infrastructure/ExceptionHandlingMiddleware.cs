using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Nensure;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hack.Web
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            Ensure.NotNull(logger);
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AssertionException ex)
            {
                await SetResponse(context.Request, context.Response, StatusCodes.Status400BadRequest, ex);
            }
            catch (ValidationException ex)
            {
                await SetResponse(context.Request, context.Response, StatusCodes.Status400BadRequest, ex);
            }
            catch (Exception ex)
            {
                await SetResponse(context.Request, context.Response, StatusCodes.Status500InternalServerError, ex);
            }
        }

        private async Task SetResponse(HttpRequest request, HttpResponse response, int statusCode, Exception exception)
        {
            Ensure.NotNull(response, exception);
            switch (statusCode)
            {
                case StatusCodes.Status400BadRequest:
                    _logger.LogWarning(exception, $"Status code: {statusCode}, Request: {await FormatRequest(request)}");
                    break;

                case StatusCodes.Status500InternalServerError:
                    _logger.LogError(exception, $"Status code: {statusCode}, Request: {await FormatRequest(request)}");
                    break;
            }
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(exception));
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
    }
}