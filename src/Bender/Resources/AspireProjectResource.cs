namespace Bender;

[DebuggerDisplay("Project: {Name,nq}")]
public sealed class AspireProjectResource : AspireResource,
    IAspireParsable<(string Name, AspireJsonModel.Resource Resource), AspireProjectResource>
{
    public required AspireString Path { get; init; }
    public required Dictionary<string, AspireString> EnvironmentVariables { get; init; }
    public required List<AspireString> Args { get; init; }
    public required AspireBindings Bindings { get; init; }

    public static AspireProjectResource Parse((string Name, AspireJsonModel.Resource Resource) parsable)
    {
        return new AspireProjectResource
        {
            Name = parsable.Name,
            Type = parsable.Resource.Type,
            Path = AspireString.Parse(parsable.Resource.Path),
            EnvironmentVariables = parsable.Resource.Env.ToAspireStringDictionary(),
            Args = parsable.Resource.Args.ToAspireStringList(),
            Bindings = AspireBindings.Parse(parsable.Resource.Bindings),
        };
    }

    protected override object? Match(string path)
    {
        return path switch
        {
            "path" => Path,
            "bindings" => Bindings,
            _ => null,
        };
    }
}