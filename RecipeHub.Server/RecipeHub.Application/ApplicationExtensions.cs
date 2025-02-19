using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RecipeHub.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddRecipeHubApi(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        // MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(currentAssembly);
        });

        // FluentValidation
        services.AddValidatorsFromAssembly(currentAssembly, includeInternalTypes: true);

        // AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}