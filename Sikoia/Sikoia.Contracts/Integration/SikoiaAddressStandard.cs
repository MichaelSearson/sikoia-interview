namespace Sikoia.Contracts.Integration
{
    public sealed class SikoiaAddressStandard
    {
        public SikoiaAddressStandard(string? street, string? city, string? country, string? postCode)
        {
            Street = street;
            City = city;
            Country = country;
            PostCode = postCode;
        }

        public string? Street { get; }

        public string? City { get; }

        public string? Country { get; }

        public string? PostCode { get; }
    }
}