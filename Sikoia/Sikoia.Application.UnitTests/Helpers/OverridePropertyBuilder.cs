using AutoFixture.Kernel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Sikoia.Application.Tests.Helpers
{
    internal class OverridePropertyBuilder<T, TProp> : ISpecimenBuilder
    {
        private readonly PropertyInfo propertyInfo;
        private readonly TProp? value;

        public OverridePropertyBuilder(Expression<Func<T, TProp>> expr, TProp? value)
        {
            propertyInfo = (expr.Body as MemberExpression)?.Member as PropertyInfo ?? throw new InvalidOperationException("Invalid property expression");
            this.value = value;
        }

        public object? Create(object request, ISpecimenContext context)
        {
            if (request is not ParameterInfo pi)
                return new NoSpecimen();

            var camelCase = Regex.Replace(propertyInfo.Name, @"(\w)(.*)",
                m => m.Groups[1].Value.ToLower() + m.Groups[2]);

            if (pi.ParameterType != typeof(TProp) || pi.Name != camelCase)
                return new NoSpecimen();

            return value;
        }
    }
}