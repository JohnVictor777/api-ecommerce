using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Context;

namespace ApiEcommerce.Shared.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                // Adiciona propriedades da requisição ao contexto de log para logs subsequentes
                using (LogContext.PushProperty("RequestMethod", context.Request.Method))
                using (LogContext.PushProperty("RequestPath", context.Request.Path))
                {
                    await _next(context);
                }
            }
            finally
            {
                sw.Stop();
                var statusCode = context.Response?.StatusCode;
                // Loga a requisição com informações básicas
                _logger.LogInformation("Requisição {RequestMethod} {RequestPath} retornou {StatusCode} em {ElapsedMs} ms",
                    context.Request.Method,
                    context.Request.Path,
                    statusCode,
                    sw.ElapsedMilliseconds);
            }
        }
    }
}