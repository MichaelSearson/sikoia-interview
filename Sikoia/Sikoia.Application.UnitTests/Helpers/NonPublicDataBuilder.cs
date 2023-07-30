using AutoFixture;

namespace Sikoia.Application.Tests.Helpers
{
    internal static class NonPublicDataBuilder
    {
        public static FixtureCustomisation<T> For<T>()
        {
            return new(new Fixture());
        }
    }
}