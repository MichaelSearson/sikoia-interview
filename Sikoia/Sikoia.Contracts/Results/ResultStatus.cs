namespace Sikoia.Integration.Results
{
    public sealed class ResultStatus<TResult> where TResult : class
    {
        public ResultStatus(TResult result)
        {
            Result = result;
        }

        public ResultStatus(string errorMessage)
        {
            if (errorMessage is null)
            {
                throw new ArgumentNullException(nameof(errorMessage), "Must provide a non-null error message");
            }

            Error = errorMessage;
        }

        public bool Success => string.IsNullOrEmpty(Error);

        public string? Error { get; }

        public TResult? Result { get; }
    }
}