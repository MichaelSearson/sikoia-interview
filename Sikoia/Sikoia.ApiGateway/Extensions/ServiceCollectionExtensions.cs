using Microsoft.OpenApi.Models;
using Sikoia.Application.Aggregators;
using Sikoia.Application.Configuration;
using Sikoia.Application.CrossCuttingConcerns;
using Sikoia.Application.Factories;
using Sikoia.Application.Providers;
using Sikoia.Application.Queries;
using Sikoia.Application.Queries.Company;
using Sikoia.Application.ReadModels;
using Sikoia.Application.Services.Jurisdiction;
using Sikoia.Integration.ThirdPartyA.Providers;
using Sikoia.Integration.ThirdPartyB.Providers;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Sikoia.ApiGateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            return services
                .RegisterQueries()
                .RegisterJurisdictionDependencies()
                .RegisterCustomHttpClients()
                .RegisterDataAggregators()
                .RegisterApiDependencies()
                .RegisterSwagger()
                .RegisterConfiguration(configuration);
        }

        private static IServiceCollection RegisterQueries(this IServiceCollection services)
        {
            services.AddScoped<IAsyncQueryHandler<FindCompanyByJurisdictionQuery, CompanyReadModel>, FindCompanyByJurisdictionQueryHandler>();
            services.Decorate<IAsyncQueryHandler<FindCompanyByJurisdictionQuery, CompanyReadModel>, CacheQueryHandlerDecorator<FindCompanyByJurisdictionQuery, CompanyReadModel>>();
            return services;
        }

        private static IServiceCollection RegisterJurisdictionDependencies(this IServiceCollection services)
        {
            services.AddTransient<IJurisdictionServiceFactory, JurisdictionServiceFactory>();
            services.AddTransient<GermanJurisdictionService>();
            services.AddTransient<BritishJurisdictionService>();
            services.AddTransient<DutchJurisdictionService>();
            return services;
        }

        private static IServiceCollection RegisterCustomHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<IThirdPartyAHttpProvider, ThirdPartyAHttpProvider>();
            services.AddHttpClient<IThirdPartyBHttpProvider, ThirdPartyBHttpProvider>();
            return services;
        }

        private static IServiceCollection RegisterDataAggregators(this IServiceCollection services)
        {
            services.AddScoped<ICompanyStandardAggregator, CompanyStandardAggregator>();
            return services;
        }

        private static IServiceCollection RegisterApiDependencies(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddEndpointsApiExplorer();

            return services;
        }

        private static IServiceCollection RegisterConfiguration(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<ThirdPartyAOptions>(configuration.GetSection("ThirdPartyASettings"));
            services.Configure<ThirdPartyBOptions>(configuration.GetSection("ThirdPartyBSettings"));
            return services;
        }

        private static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Sikoia Gateway API",
                    Description = "Provides a consistent standard endpoint for accessing multiple third party sources of data"
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }
    }
}