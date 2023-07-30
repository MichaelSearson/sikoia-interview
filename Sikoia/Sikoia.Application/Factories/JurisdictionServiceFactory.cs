using Sikoia.Application.Enums;
using Sikoia.Application.Services.Jurisdiction;

namespace Sikoia.Application.Factories
{
    public sealed class JurisdictionServiceFactory : IJurisdictionServiceFactory
    {
        private static readonly Dictionary<JurisdictionCode, Type> jurisdictionToServiceMap = new()
        {
            {JurisdictionCode.UK, typeof(BritishJurisdictionService) },
            {JurisdictionCode.NL, typeof(DutchJurisdictionService) },
            {JurisdictionCode.DE, typeof(GermanJurisdictionService) },
        };

        private readonly IServiceProvider serviceProvider;

        public JurisdictionServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJurisdictionService GetJurisdiction(JurisdictionCode jurisdiction)
        {
            var mappedType = jurisdictionToServiceMap[jurisdiction];

            if (serviceProvider.GetService(mappedType) is not IJurisdictionService serivce)
            {
                throw new NullReferenceException($"Could not resolve a service for the provided jurisdiction: {jurisdiction}. This should never happen.");
            }

            return serivce;
        }
    }
}