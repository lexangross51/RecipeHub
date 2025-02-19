namespace RecipeHub.Domain.Models;

public record Measure
{
    public string Unit { get; init; } = default!;

    public double Value { get; init; }
}