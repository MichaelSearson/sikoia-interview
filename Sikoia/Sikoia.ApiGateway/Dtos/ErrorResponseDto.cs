namespace Sikoia.ApiGateway.Dtos
{
    public sealed class ErrorResponseDto
    {
        public ErrorResponseDto(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}