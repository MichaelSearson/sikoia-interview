namespace Sikoia.Integration.ThirdPartyA.Dtos
{
    internal class ErrorResponseDto
    {
        public int Status { get; set; }
        public string? Type { get; set; }

        public string? Title { get; set; }

        public string? Detail { get; set; }
    }
}