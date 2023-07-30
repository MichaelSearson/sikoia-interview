using Sikoia.Contracts.Integration;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Sikoia.Application.Tests.DataGenerators
{
    [ExcludeFromCodeCoverage]
    public class SikoiaCompanyStandardInvalidCollectionGenerator : IEnumerable<object?[]>
    {
        private readonly List<object?[]> _data = new()
        {
            new object?[] { default(List<SikoiaCompanyStandard>) },
            new object?[] { Enumerable.Empty<SikoiaCompanyStandard>().ToList() },
        };

        public IEnumerator<object?[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}