namespace Sikoia.Contracts.Integration
{
    public class SikoiaRelatedCompanyStandard
    {
        public SikoiaRelatedCompanyStandard(
            string? name,
            DateTime? dateFrom,
            DateTime? dateTo,
            string? type,
            decimal? ownership,
            string? country)
        {
            Name = name;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Type = type;
            Ownership = ownership;
            Country = country;
        }

        public string? Name { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string? Type { get; set; }

        public decimal? Ownership { get; set; }

        public string? Country { get; set; }
    }
}