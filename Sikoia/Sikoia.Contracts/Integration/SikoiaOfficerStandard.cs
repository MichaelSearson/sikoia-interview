namespace Sikoia.Contracts.Integration
{
    public sealed class SikoiaOfficerStandard
    {
        public SikoiaOfficerStandard(
            string? firstName,
            string? middleNames,
            string? lastName,
            string? name,
            DateTime? dateFrom,
            DateTime? dateTo,
            string? role,
            SikoiaPartialDateStandard? dateOfBirth)
        {
            FirstName = firstName;
            MiddleNames = middleNames;
            LastName = lastName;
            Name = name;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Role = role;
            DateOfBirth = dateOfBirth;
        }

        public string? FirstName { get; }

        public string? MiddleNames { get; }

        public string? LastName { get; }

        public string? Name { get; }

        public DateTime? DateFrom { get; }

        public DateTime? DateTo { get; }

        public string? Role { get; }

        public SikoiaPartialDateStandard? DateOfBirth { get; }
    }
}