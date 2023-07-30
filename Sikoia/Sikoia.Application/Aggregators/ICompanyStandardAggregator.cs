using Sikoia.Application.ReadModels;
using Sikoia.Contracts.Integration;

namespace Sikoia.Application.Aggregators
{
    public interface ICompanyStandardAggregator
    {
        public CompanyReadModel Aggregate(List<SikoiaCompanyStandard> companies);
    }
}