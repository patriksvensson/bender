namespace Bender;

public abstract class AspireResource : IAspireMatchable
{
    public required string Name { get; init; }
    public required string Type { get; init; }

    object? IAspireMatchable.Match(string path)
    {
        return Match(path);
    }

    protected abstract object? Match(string path);
}