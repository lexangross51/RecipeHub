namespace RecipeHub.WebServer;

internal static class MemoryCachingSettings
{
    public static TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromMinutes(5);

    public static TimeSpan CheckExpiredFrequency { get; set; } = TimeSpan.FromMinutes(5);
}