namespace Sikoia.Contracts.Integration
{
    public sealed class SikoiaOwnerStandard
    {
        public SikoiaOwnerStandard(
            string? firstName,
            string? middleNames,
            string? lastName,
            string? name,
            DateTime? dateFrom,
            DateTime? dateTo,
            string? ownershipType,
            decimal sharesHeld,
            SikoiaPartialDateStandard? dateOfBirth)
        {
            FirstName = firstName;
            MiddleNames = middleNames;
            LastName = lastName;
            Name = name;
            DateFrom = dateFrom;
            DateTo = dateTo;
            OwnershipType = ownershipType;
            SharesHeld = sharesHeld;
            DateOfBirth = dateOfBirth;
        }

        public string? FirstName { get; set; }

        public string? MiddleNames { get; set; }

        public string? LastName { get; set; }

        public string? Name { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string? OwnershipType { get; set; }

        public decimal SharesHeld { get; set; }

        public SikoiaPartialDateStandard? DateOfBirth { get; set; }
    }
}