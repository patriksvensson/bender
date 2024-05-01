namespace Bender;

[DebuggerDisplay("Value: {Name,nq}")]
public sealed class AspireValueResource : AspireResource,
    IAspireParsable<(string Name, AspireJsonModel.Resource Resource), AspireValueResource>
{
    public required AspireString ConnectionString { get; init; }

    public static AspireValueResource Parse((string Name, AspireJsonModel.Resource Resource) parsable)
    {
        return new AspireValueResource
        {
            Name = parsable.Name,
            Type = parsable.Resource.Type,
            ConnectionString = AspireString.Parse(parsable.Resource.ConnectionString),
        };
    }

    protected override object? Match(string path)
    {
        return path == "connectionString"
            ? ConnectionString
            : null;
    }
}