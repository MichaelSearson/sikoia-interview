namespace Sikoia.Contracts.Integration
{
    public struct SikoiaPartialDateStandard
    {
        public SikoiaPartialDateStandard(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public int Year { get; set; }

        public int Month { get; set; }
    }
}