using RecipeHub.Application;
using RecipeHub.Persistence;

namespace RecipeHub.WebServer;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services
            .AddRecipeHubApi()
            .AddMemoryCache(options => options.ExpirationScanFrequency = MemoryCachingSettings.CheckExpiredFrequency)
            .AddInfrastructure(connectionString)
            .AddControllers()
            .AddJsonOptions(builder =>
            {
                builder.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(ep => ep.MapControllers());
    }
}