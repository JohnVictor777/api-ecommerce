using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiEcommerce.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ApiEcommerce.Shared.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _nextDelegate = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _nextDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exceção não tratada em {Path}", context.Request.Path);
                await HandleExceptionAsync(context, ex);
            }
        }

        // Método para lidar com exceções e retornar respostas JSON apropriadas com base no tipo de exceção
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Erro interno no servidor";
            var detail = "Ocorreu um erro inesperado.";

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    detail = notFoundException.Message;
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = validationException.Message;
                    detail = validationException.Message;
                    break;
                case DbUpdateConcurrencyException concurrencyException:
                    statusCode = HttpStatusCode.Conflict;
                    message = "Conflito de dados";
                    detail = "O registro que você tentou atualizar foi modificado ou excluído por outro usuário. Por favor, tente novamente.";
                    break;
                default:
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                message = message,
                detail = detail
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
