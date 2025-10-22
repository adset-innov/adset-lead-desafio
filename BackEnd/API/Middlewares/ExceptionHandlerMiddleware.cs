using API.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string titulo = "Ocorreu um erro inesperado.";
            string detalhes = exception.Message;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    titulo = "Recurso não encontrado.";
                    break;
                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    titulo = "O Recurso atual contém dados que conflitam com outros";
                    break;


                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    titulo = "Acesso não autorizado.";
                    break;

                case Microsoft.EntityFrameworkCore.DbUpdateException:
                    statusCode = HttpStatusCode.Conflict;
                    titulo = "Erro ao salvar alterações no banco de dados.";
                    break;


                case IOException:
                    statusCode = HttpStatusCode.InternalServerError;
                    titulo = "Erro ao acessar o sistema de arquivos.";
                    break;

                case ValidationException:
                    statusCode = HttpStatusCode.UnprocessableEntity;
                    titulo = "Erro de validação.";
                    break;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            var erroDetalhes = new
            {
                type = $"https://httpstatuses.com/{(int)statusCode}",
                titulo,
                status = (int)statusCode,
                detalhes,
                instance = context.Request.Path
            };

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(erroDetalhes, jsonOptions));
        }
    }
}
