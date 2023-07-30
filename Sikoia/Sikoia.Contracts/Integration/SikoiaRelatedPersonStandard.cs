namespace Sikoia.Contracts.Integration
{
    public sealed class SikoiaRelatedPersonStandard
    {
        public SikoiaRelatedPersonStandard(
            string? name,
            DateTime? dateFrom,
            DateTime? dateTo,
            string? type,
            decimal? ownership,
            DateTime? dateOfBirth,
            string? nationality)
        {
            Name = name;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Type = type;
            Ownership = ownership;
            DateOfBirth = dateOfBirth;
            Nationality = nationality;
        }

        public string? Name { get; }

        public DateTime? DateFrom { get; }

        public DateTime? DateTo { get; }

        public string? Type { get; }

        public decimal? Ownership { get; }

        public DateTime? DateOfBirth { get; }

        public string? Nationality { get; }
    }
}