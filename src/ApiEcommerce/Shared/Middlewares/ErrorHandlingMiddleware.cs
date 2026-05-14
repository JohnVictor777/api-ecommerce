using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiEcommerce.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ApiEcommerce.Shared.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _nextDelegate = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _nextDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

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