namespace Sikoia.Contracts.Integration
{
    public sealed class SikoiaActivityStandard
    {
        public SikoiaActivityStandard(int activityCode, string? activityDescription)
        {
            ActivityCode = activityCode;
            ActivityDescription = activityDescription;
        }

        public int ActivityCode { get; }

        public string? ActivityDescription { get; }
    }
}