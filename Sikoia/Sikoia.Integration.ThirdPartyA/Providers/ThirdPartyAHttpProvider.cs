using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sikoia.Application.Configuration;
using Sikoia.Application.Enums;
using Sikoia.Application.Providers;
using Sikoia.Contracts.Integration;
using Sikoia.Integration.Results;
using Sikoia.Integration.ThirdPartyA.Dtos;
using Sikoia.Integration.ThirdPartyA.Mapping;
using System.Text;

namespace Sikoia.Integration.ThirdPartyA.Providers
{
    public sealed class ThirdPartyAHttpProvider : IThirdPartyAHttpProvider
    {
        private readonly IOptions<ThirdPartyAOptions> settings;
        private readonly HttpClient httpClient;

        public ThirdPartyAHttpProvider(IOptions<ThirdPartyAOptions> settings, HttpClient httpClient)
        {
            this.settings = settings;
            this.httpClient = httpClient;
        }

        public async Task<ResultStatus<SikoiaCompanyStandard>> GetCompanyDataAsync(JurisdictionCode jurisdictionCode, int companyNumber)
        {
            try
            {
                var requestUrl = BuildRequestUrl(jurisdictionCode, companyNumber);
                var result = await httpClient.GetAsync(requestUrl);

                if (!result.IsSuccessStatusCode)
                {
                    var errorResponseContent = await result.Content.ReadAsStringAsync();
                    var errorDto = JsonConvert.DeserializeObject<ErrorResponseDto>(errorResponseContent);

                    var reason = errorDto?.Detail ?? result.ReasonPhrase;

                    return new ResultStatus<SikoiaCompanyStandard>($"Non-success status code returned from {nameof(ThirdPartyAHttpProvider)}. Reason: {reason}");
                }

                var responseContent = await result.Content.ReadAsStringAsync();

                var companyDataDto = JsonConvert.DeserializeObject<CompanyDataDto>(responseContent);

                if (companyDataDto is not null)
                {
                    var companyStandard = CompanyDataDtoToStandardMapper.Map(companyDataDto);
                    return new ResultStatus<SikoiaCompanyStandard>(companyStandard);
                }
            }
            catch (Exception)
            {
                // Log exception etc...

                // For demonstration purposes catch everything. Realistically we'll want to only catch things
                // related to the above failing we can realistically recover from
                return new ResultStatus<SikoiaCompanyStandard>($"Something went wrong trying to get results from {nameof(ThirdPartyAHttpProvider)}");
            }

            return new ResultStatus<SikoiaCompanyStandard>($"Could not deserialize response in {nameof(ThirdPartyAHttpProvider)}");
        }

        // Could pull this out into its own testable concern
        private string BuildRequestUrl(JurisdictionCode jurisdictionCode, int companyNumber)
        {
            var requestBuilder = new StringBuilder();
            requestBuilder.Append(settings.Value.BaseUrl);
            requestBuilder.Append('/');
            requestBuilder.Append("v1");
            requestBuilder.Append('/');
            requestBuilder.Append("company");
            requestBuilder.Append('/');
            requestBuilder.Append(jurisdictionCode.ToString().ToLower());
            requestBuilder.Append('/');
            requestBuilder.Append(companyNumber);

            return requestBuilder.ToString();
        }
    }
}