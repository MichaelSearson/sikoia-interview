using AutoFixture;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Sikoia.Application.Tests.Helpers
{
    [ExcludeFromCodeCoverage]
    internal class FixtureCustomisation<T>
    {
        public Fixture Fixture { get; }

        public FixtureCustomisation(Fixture fixture)
        {
            Fixture = fixture;
        }

        public FixtureCustomisation<T> With<TProp>(Expression<Func<T, TProp>> expr, TProp value)
        {
            Fixture.Customizations.Add(new OverridePropertyBuilder<T, TProp>(expr, value));
            return this;
        }

        public FixtureCustomisation<T> Without<TProp>(Expression<Func<T, TProp>> expr)
        {
            Fixture.Customizations.Add(new OverridePropertyBuilder<T, TProp>(expr, default));
            return this;
        }

        public T Create() => Fixture.Create<T>();
    }
}