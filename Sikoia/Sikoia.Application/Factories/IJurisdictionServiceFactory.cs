using Sikoia.Application.Enums;
using Sikoia.Application.Services.Jurisdiction;

namespace Sikoia.Application.Factories
{
    public interface IJurisdictionServiceFactory
    {
        IJurisdictionService GetJurisdiction(JurisdictionCode jurisdiction);
    }
}