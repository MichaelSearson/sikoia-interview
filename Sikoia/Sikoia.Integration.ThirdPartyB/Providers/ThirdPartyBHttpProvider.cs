using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sikoia.Application.Configuration;
using Sikoia.Application.Enums;
using Sikoia.Application.Providers;
using Sikoia.Contracts.Integration;
using Sikoia.Integration.Results;
using Sikoia.Integration.ThirdPartyB.Dtos;
using Sikoia.Integration.ThirdPartyB.Mapping;
using System.Text;

namespace Sikoia.Integration.ThirdPartyB.Providers
{
    public sealed class ThirdPartyBHttpProvider : IThirdPartyBHttpProvider
    {
        private readonly IOptions<ThirdPartyBOptions> settings;
        private readonly HttpClient httpClient;

        public ThirdPartyBHttpProvider(IOptions<ThirdPartyBOptions> settings, HttpClient httpClient)
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
                    return new ResultStatus<SikoiaCompanyStandard>($"Non-success status code returned from {nameof(ThirdPartyBHttpProvider)}. Reason: {result.ReasonPhrase}. Verify the request parameters and try again.");
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
                return new ResultStatus<SikoiaCompanyStandard>($"Something went wrong trying to get results from {nameof(ThirdPartyBHttpProvider)}");
            }

            return new ResultStatus<SikoiaCompanyStandard>($"Could not deserialize response in {nameof(ThirdPartyBHttpProvider)}");
        }

        private string BuildRequestUrl(JurisdictionCode jurisdictionCode, int companyNumber)
        {
            var requestBuilder = new StringBuilder();
            requestBuilder.Append(settings.Value.BaseUrl);
            requestBuilder.Append('/');
            requestBuilder.Append("v1");
            requestBuilder.Append('/');
            requestBuilder.Append("company-data?jurisdictionCode=");
            requestBuilder.Append(jurisdictionCode.ToString().ToLower());
            requestBuilder.Append("&companyNumber=");
            requestBuilder.Append(companyNumber);

            return requestBuilder.ToString();
        }
    }
}