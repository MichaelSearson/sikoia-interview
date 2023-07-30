using Newtonsoft.Json;
using Sikoia.ApiGateway.Dtos;
using System.Net;

namespace Sikoia.ApiGateway.Middleware
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception)
            {
                // Log exception etc...
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorDto = new ErrorResponseDto("Something went wrong processing your request. Please try again later.");
                var jsonErrorResponse = JsonConvert.SerializeObject(errorDto);

                await context.Response.WriteAsync(jsonErrorResponse);
            }
        }
    }
}