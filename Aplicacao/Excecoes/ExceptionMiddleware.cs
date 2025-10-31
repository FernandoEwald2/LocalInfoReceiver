
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Aplicacao.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ExceptionMessage response;

            if (ex is LocalException localEx)
            {
                // usa o seu objeto customizado
                response = localEx.Exception;
                context.Response.StatusCode = int.TryParse(localEx.Exception.Codigo, out var code)
                    ? code
                    : (int)HttpStatusCode.BadRequest;
            }
            else
            {
                // fallback para exceções não tratadas
                response = new ExceptionMessage
                {
                    Codigo = ((int)HttpStatusCode.InternalServerError).ToString(),
                    Mensagem = "Erro inesperado."
                };
            }

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

}

