using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeHub.Domain.Storage.Abstractions;
using RecipeHub.Persistence.Storage;
using System.Reflection;

namespace RecipeHub.Persistence;

public static class DbDiExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), 
                "Невозможно создать подключение к БД, т.к. строка подключения не задана");
        }

        services
            .AddDbContext<RecipeHubContext>(opt => opt.UseNpgsql(connectionString))
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var baseRepType = typeof(IRepository<,>);
        var repTypes = (Assembly
            .GetAssembly(baseRepType)?
            .GetTypes()
            .Where(t => t.IsInterface && 
                t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseRepType))) ?? 
            throw new FileNotFoundException($"Не найдена сборка, содержащая {baseRepType}");

        var currentAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

        foreach (var repType in repTypes)
        {
            var implementation = currentAssemblyTypes.FirstOrDefault(t => t.IsClass && repType.IsAssignableFrom(t));

            if (implementation == null)
            {
                continue;
            }

            services.AddScoped(repType, implementation);
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}