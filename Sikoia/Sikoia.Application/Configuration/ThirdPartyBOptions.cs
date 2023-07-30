using System.Diagnostics.CodeAnalysis;

namespace Sikoia.Application.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ThirdPartyBOptions
    {
        public string BaseUrl { get; set; } = null!;
    }
}